using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCalendar
{
    public partial class frm_OUs : Form
    {
        public frm_OUs()
        {
            InitializeComponent();
        }

        private void frm_OUs_Load(object sender, EventArgs e)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://myServer:1111/DC=sh,DC=sh,DC=dom");
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            mySearcher.Filter = ("(objectClass=organizationalUnit)");
            mySearcher.SizeLimit = int.MaxValue;
            mySearcher.PageSize = int.MaxValue;

            foreach (SearchResult resEnt in mySearcher.FindAll())
            {
                string OUName = resEnt.GetDirectoryEntry().Name;
                Console.WriteLine(OUName);
            }

            mySearcher.Dispose();
            entry.Dispose();
        }
    }
}
