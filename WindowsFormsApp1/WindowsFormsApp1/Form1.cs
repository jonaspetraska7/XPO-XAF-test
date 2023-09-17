using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.XPO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private XPCollection _xpCollection;

        public Form1()
        {
            InitializeComponent();
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var student = gridView1.GetFocusedRow() as Student;
            if (student != null)
            {
                txtCode.Text = student.Code;
                txtFullName.EditValue = student.Name;
                txtAddress.EditValue = student.Address;
                txtDate.EditValue = student.DateOfBirth;
                txtGender.EditValue = student.Gender;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = MSSqlConnectionProvider.GetConnectionString(".\\SQLEXPRESS", "XPOtest", "XPOtest", "XPOCRUDDemo");
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);

            LoadData();
        }

        public void LoadData()
        {
            _xpCollection = new XPCollection(typeof(Student));
            gridControl1.DataSource = _xpCollection;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var dlg = XtraMessageBox.Show($"Do you want <b><color=red>Delete</color></b> student selected?",
                "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, DevExpress.Utils.DefaultBoolean.True);
            if(dlg == DialogResult.Yes)
            {
                using (var uow = new UnitOfWork())
                {
                    var list = gridView1.GetSelectedRows().Select(x => gridView1.GetRow(x) as Student).ToList();

                    var students = uow.Query<Student>().AsEnumerable().Where(s => list.Any(x => s.Code == x.Code)).ToList();
                    uow.Delete(students);
                    uow.CommitChanges();
                    _xpCollection.Reload();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (var uow = new UnitOfWork())
            {
                var student = new Student(uow);
                student.Code = txtCode.Text;
                student.Name = txtFullName.Text;
                student.DateOfBirth = DateTime.Parse(txtDate.EditValue.ToString());
                student.Address = txtAddress.Text;
                student.Gender = txtGender.Text;

                var isExisted = uow.Query<Student>()
                                .Where(x => x.Code == txtCode.Text).ToList().Count > 0;
                if (isExisted)
                {
                    XtraMessageBox.Show($"Student with code: {student.Code} already exists.");
                    txtCode.Focus();
                    return;
                }

                uow.CommitChanges();
                _xpCollection.Reload();
                XtraMessageBox.Show($"Save Successful!", "Message");
            }
        }
    }
}
