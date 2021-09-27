using Microsoft.GroupPolicy;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalendar
{
    class cls_network
    {
        private static string sDCName = "";
        private static string sDCExtention = "";
        private static string sOU = string.Empty;
        private static GPDomain domain = null;

        static cls_network()
        {
            try
            {
                GetFQDN(ref sDCName, ref sDCExtention);

                domain = new GPDomain(sDCName + "." + sDCExtention);
                sOU = "dc=" + sDCName + ",dc=" + sDCExtention;
            }
            catch (Exception ex)
            {
                cls_utility.Log("GPO ERROR. GETGPDomain(). " + ex.Message);
            }
        }

        public static void ShareAFolder(string servername, string filepath, string sharename)
        {
            try
            {

                if (Directory.Exists(@"\\" + servername + @"\" + sharename))
                {
                    cls_utility.Log("The folder \"" + sharename + "\" is already shared. ");
                    return;
                }

                // assemble the string so the scope represents the remote server
                string scope = string.Format("\\\\{0}\\root\\cimv2", servername);

                // connect to WMI on the remote server
                ManagementScope ms = new ManagementScope(scope);

                // create a new instance of the Win32_Share WMI object
                ManagementClass cls = new ManagementClass("Win32_Share");

                // set the scope of the new instance to that created above
                cls.Scope = ms;

                // assemble the arguments to be passed to the Create method
                object[] methodargs = { filepath, sharename, "0" };

                // invoke the Create method to create the share
                object result = cls.InvokeMethod("Create", methodargs);

                cls_utility.Log("Shring Folder.................. OK ");
            }
            catch (SystemException e)
            {
                cls_utility.Log("Error attempting to create share. " + e.Message);
            }

        }

        public static void SetFolderPermissions(string sFolderPath, WellKnownSidType sIdentity, FileSystemRights rights, AccessControlType type)
        {
            try
            {
                var Info = new DirectoryInfo(sFolderPath);
                var Security = Info.GetAccessControl(AccessControlSections.Access);
                var sid = new SecurityIdentifier(sIdentity, null);

                Security.AddAccessRule(
                    new FileSystemAccessRule(
                        sid, rights,
                        InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                        PropagationFlags.None,
                        type));
                Info.SetAccessControl(Security);

                cls_utility.Log("Setting Shared Folder Permissions.................. OK ");

            }
            catch (Exception ex)
            {
                cls_utility.Log("Error attempting to set shared permissions. " + ex.Message);
            }
        }
       
        public static void DeleteGPOLink(string[] sGPOsDisplayName)
        {            
            try
            {                                
                Som som = domain.GetSom(sOU);
                GpoLinksCollection GPOLinks = som.GpoLinks;
                foreach (GpoLink GPOLink in GPOLinks)
                {
                    int nIndex = Array.IndexOf(sGPOsDisplayName, GPOLink.DisplayName);
                    if (nIndex != -1)
                    {                        
                        GPOLink.Delete();
                    }
                }
            }
            catch (MultipleGroupPolicyObjectsFoundException ex)
            {
                // Multiple GPOs exist with the specified Display Name
                cls_utility.Log("Multiple GPOs exist with the specified Display Name. " + ex.Message);                
            }
            catch (System.ArgumentException ex)
            {
                cls_utility.Log("GPO not found for deletion. " + ex.Message);
            }
        }

        public static void DeleteGPO(string sGPODisplayName)
        {            
            Gpo gpo_background = null;
            
            try
            {
                // first delete the gpo itself
                gpo_background = domain.GetGpo(sGPODisplayName);
                gpo_background.Delete();                

                // then delete all links related to August-HCalendar GPOs
                DeleteGPOLink(new string[] { sGPODisplayName });
            }
            catch (MultipleGroupPolicyObjectsFoundException ex)
            {
                // Multiple GPOs exist with the specified Display Name

                cls_utility.Log("Multiple GPOs exist with the specified Display Name. " + ex.Message);
                gpo_background.Delete();
            }
            catch (System.ArgumentException ex)
            {                
                cls_utility.Log("GPO not found for deletion. " + ex.Message);
            }
        }

        public static void CreateGPOLink(Gpo gpo)
        {
            try
            {                
                // Link the GPO
                Som som = domain.GetSom(sOU);
                som.LinkGpo(-1, gpo);
                cls_utility.Log("GPO linking..................OK");
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Linking GPO. " + ex.Message);
            }
        }

        public static void ManipulateGPO(string sGPODisplayName, string sSecurityFiltering)
        {
            
            try
            {
               
                Gpo gpo_background = null;

                try
                {
                    gpo_background = domain.GetGpo(sGPODisplayName);

                    cls_utility.Log("Getting GPO................OK ");
                }
                catch (MultipleGroupPolicyObjectsFoundException ex)
                {
                    // Multiple GPOs exist with the specified Display Name

                    cls_utility.Log("Multiple GPOs exist with the specified Display Name. " + ex.Message);
                    gpo_background.Delete();
                }
                catch (System.ArgumentException ex)
                {
                    // GPO not found so create it                    
                    gpo_background = domain.CreateGpo(sGPODisplayName);

                    cls_utility.Log("GPO not found so create it. " + ex.Message);
                }

                try
                {
                    // Set up the GPSearchCriteria to find the most recent backup.
                    GPSearchCriteria searchCriteria = new GPSearchCriteria();
                    searchCriteria.Add(SearchProperty.GpoDisplayName, SearchOperator.Equals, sGPODisplayName);
                    searchCriteria.Add(SearchProperty.MostRecentBackup, SearchOperator.Equals, true);

                    // Get a BackupDirectory object
                    BackupDirectory backupDir = new BackupDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory , @"Res\backups"), BackupType.Gpo);

                    // Obtain the backup collection that matches the search criteria
                    GpoBackupCollection backups = backupDir.SearchGpoBackups(searchCriteria);
                    GpoBackup gpoBackup = null;
                    // Check for no backup collection or empty collection (no backups)
                    if ((backups == null) || (backups.Count <= 0))
                    {
                        // No backups were found with the specified search criteria.
                        cls_utility.Log("No GPO backups were found with the specified search criteria. ");
                    }
                    else
                    {
                        // Backups[0] is a GPBackup object representing the most recent backup of the requested GPO
                        gpoBackup = backups[0];

                        cls_utility.Log("Backup loading................... OK");
                    }

                    GPMigrationTable migrationTable = null; //new GPMigrationTable(@"c:\backups\myMigrationTable.mig");

                    GPStatusMessageCollection statusMessages = null;
                    if (migrationTable != null)
                    {
                        gpo_background.Import(gpoBackup, migrationTable, out statusMessages);
                    }
                    else
                    {
                        gpo_background.Import(gpoBackup, out statusMessages);

                        cls_utility.Log("No migration Table where found. ");
                        cls_utility.Log("Importing GPO Settings.........................OK ");
                    }
                }
                catch (Exception ex)
                {
                    cls_utility.Log("Error in importing GPO backup. " + ex.Message);
                }

                try
                {
                    // Add or remove Principals
                    GPPermissionCollection gppc = gpo_background.GetSecurityInfo();

                    // retriving all non-built-in groups
                    List<string> groupsAll = ListAllDomainGroups();

                    // First Remove All Groups
                    try
                    {
                        foreach (string sGroup in groupsAll)
                        {
                            gppc.RemoveTrustee(sGroup);
                        }
                        gppc.RemoveTrustee("Authenticated Users");
                    }
                    catch (Exception ex)
                    {
                        cls_utility.Log("Error in removing all groups from security filtering. " + ex.Message);
                    }

                    // then add User-Selected Groups
                    try
                    {
                        string[] sGroups = sSecurityFiltering.Split(new char[] { ';' });
                        foreach (string sGroup in sGroups)
                        {
                            string sSecFiltExp = string.Empty;
                            if (sGroup.ToLower() == "(non)")
                                break;
                            if (!sGroup.ToLower().Contains("authenticated"))
                                sSecFiltExp = sDCName + "\\" + sGroup;
                            else
                                sSecFiltExp = sGroup;
                            GPPermission gp = new GPPermission(sSecFiltExp, GPPermissionType.GpoApply, false);
                            gppc.Add(gp);
                        }

                    }
                    catch (Exception ex)
                    {
                        cls_utility.Log("Error in Applying Security Filtering. Default \"Authenticated Users\" will be applied only. " + ex.Message);
                    }

                    gpo_background.SetSecurityInfo(gppc);
                    cls_utility.Log("GPO Adding Principals..................OK");
                }
                catch (Exception ex)
                {
                    cls_utility.Log("Error in Adding Principals. " + ex.Message);
                }

                CreateGPOLink(gpo_background);

            }
            catch (Exception ex)
            {
                cls_utility.Log("GPO error. " + ex.Message);
            }
        }

        public static void GetFQDN(ref string sDCName, ref string sDCExtension)
        {
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;

            int i = domainName.IndexOf(".");
            sDCName = domainName.Substring(0, i);
            sDCExtension = domainName.Substring(i + 1);
        }

        public static List<String> ListAllDomainGroups()
        {

            List<String> groups = new List<string>();

            try
            {
                // create your domain context
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

                // define a "query-by-example" principal - here, we search for a GroupPrincipal 
                GroupPrincipal qbeGroup = new GroupPrincipal(ctx);

                // create your principal searcher passing in the QBE principal    
                PrincipalSearcher srch = new PrincipalSearcher(qbeGroup);

                // find all matches
                foreach (var found in srch.FindAll())
                {
                    // do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          
                    if (!found.DistinguishedName.ToLower().Contains("cn=builtin") && !found.DistinguishedName.ToLower().Contains("cn=users"))
                        groups.Add(found.ToString());
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Getting Groups list from the AD. " + ex.Message);
            }

            return groups;
        }

    }
}