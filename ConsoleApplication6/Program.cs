using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication6
{

    class HIMS
    {
        static List<Physician> physicians = new List<Physician>();
        static List<ContactInfo> contactInfor = new List<ContactInfo>();
        static List<Specialization> specializations = new List<Specialization>();

        public static void Main()
        {
            string myChoice;

            do
            {
                myChoice = getChoice();

                switch (myChoice)
                {
                    case "1":
                        AddPhysician();
                        Console.Clear();
                        break;

                    case "2":
                        DeletePhysician();
                        break;

                    case "3":
                        UpdatePhysician();
                        break;

                    case "4":
                        ViewAllPhysician();
                        break;

                    case "5":
                        GetPhysicianById();
                        break;

                    case "6":
                        Console.WriteLine("Are you sure you want to exit application?");
                        break;

                    default:
                        Reusable.InvalidInput("Choice is Invalid");
                        break;
                }


                Console.WriteLine();
                Console.Write("Press enter key to continue...");
                Console.ReadLine();
                Console.WriteLine();

            } while (myChoice != "6");
        }

        private static string getChoice()
        {
            string myChoice;
            Console.Clear();
            Console.WriteLine("Welcome to PW Hospital Information Management System!");
            Console.WriteLine("=====================================================");
            Console.WriteLine(" MENU ");
            Console.WriteLine("=====================================================");
            Console.WriteLine(" 1. Add Physician records");
            Console.WriteLine(" 2. Delete Physician records");
            Console.WriteLine(" 3. Update Physician records");
            Console.WriteLine(" 4. View all Physician records");
            Console.WriteLine(" 5. Find a Physician by ID");
            Console.WriteLine(" 6. Exit");


            Console.Write("Please select an option from the menu: ");
            myChoice = Console.ReadLine();
            Console.WriteLine();

            return myChoice;
        }

        private static void DeletePhysician()
        {
            

            try
            {
                Console.Write("Enter Id of Physician that you want to delete: ");
                var forDeletion = Reusable.GetInputWholeNum(20);

                if (physicians.Any(item => item.Id == forDeletion))
                {
                    physicians.RemoveAll(item => item.Id == forDeletion);
                    contactInfor.RemoveAll(item => item.PhysicianId == forDeletion);
                    specializations.RemoveAll(item => item.PhysicianId == forDeletion);
                }
                else
                {
                    Reusable.InvalidInput("\nPhysician id does not exist");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }



        }

        private static void UpdatePhysician()
        {
            string categoryToUpdate;
            Physician input = new Physician();
            ContactInfo con = new ContactInfo();
            Specialization spec = new Specialization();
            Console.Clear();
            Console.Write("\nEnter Id of Physician that you want to update: ");

            bool update = false;
            while (!update)
                try
                {
                    var forUpdate = Reusable.GetInputWholeNum(20);


                    if (physicians.Any(item => item.Id == forUpdate))
                    {
                        Console.WriteLine("\nWhat do you want to update? ");
                        Console.WriteLine("=====================================================");
                        Console.WriteLine(" 1. Personal Information");
                        Console.WriteLine(" 2. Contact Information");
                        Console.WriteLine(" 3. Specialization");
                        Console.Write("\n Please select an option from the menu: ");

                        categoryToUpdate = Console.ReadLine();
                        switch (categoryToUpdate)
                        {
                            case "1":
                                string itemToUpdate; 
                                itemToUpdate = UpdatePerInfoMenu();
                                switch (itemToUpdate)
                                {
                                    case "1":
                                        Reusable.UpdateInfo("Enter new First name: ");
                                        input.FirstName = Reusable.ReadLimited(20);
                                        physicians.Where(item => item.Id == forUpdate).FirstOrDefault().FirstName = input.FirstName;
                                        break;
                                    case "2":
                                        Reusable.UpdateInfo("Enter new Middle name: ");
                                        input.MiddleName = Reusable.ReadLimited(20);
                                        physicians.Where(item => item.Id == forUpdate).FirstOrDefault().MiddleName = input.MiddleName;
                                        break;
                                    case "3":
                                        Reusable.UpdateInfo("Enter new Last name: ");
                                        input.LastName = Reusable.ReadLimited(20);
                                        physicians.Where(item => item.Id == forUpdate).FirstOrDefault().LastName = input.LastName;
                                        break;
                                    case "4":
                                        Reusable.UpdateInfo("Enter new Birth date: ");

                                        bool birthdate = false;
                                        while (!birthdate)
                                        {
                                            try
                                            {

                                                input.BirthDate = Convert.ToDateTime(Console.ReadLine());
                                                birthdate = true;
                                                physicians.Where(item => item.Id == forUpdate).FirstOrDefault().BirthDate = input.BirthDate;
                                            }
                                            catch (Exception)
                                            {
                                                Reusable.InvalidInput("Please provide a valid date format: ");

                                            }
                                        }
                                        break;
                                    case "5":
                                        Reusable.UpdateInfo("Enter new Gender: ");
                                        input.Gender = Reusable.GetInputWithFormat("[FMfm]");
                                        physicians.Where(item => item.Id == forUpdate).FirstOrDefault().Gender = input.Gender;
                                        break;
                                    case "6":
                                        Reusable.UpdateInfo("Enter new Height: ");
                                        input.Height = Reusable.GetInputWithDec(4);
                                        physicians.Where(item => item.Id == forUpdate).FirstOrDefault().Height = input.Height;
                                        break;
                                    case "7":
                                        Reusable.UpdateInfo("Enter new Weight: ");
                                        input.Weight = Reusable.GetInputWithDec(4);
                                        physicians.Where(item => item.Id == forUpdate).FirstOrDefault().Weight = input.Weight;
                                        break;
                                    default:
                                        Reusable.InvalidInput("Choice is Invalid");
                                        break;
                                }
                                break;

                            case "2":
                                string contactToUpdate;
                                contactToUpdate = UpdateConInfoMenu();
                                switch (contactToUpdate)
                                {
                                    case "1":
                                        Reusable.UpdateInfo("Enter new Home Address: ");
                                        con.HomeAddress = Reusable.ReadLimited(50);
                                        contactInfor.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().HomeAddress = con.HomeAddress;
                                        break;
                                    case "2":
                                        Reusable.UpdateInfo("Enter new Home Phone: ");
                                        con.HomePhone = Reusable.GetInputWholeNum(11);
                                        contactInfor.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().HomePhone = con.HomePhone;
                                        break;
                                    case "3":
                                        Reusable.UpdateInfo("Enter new Office Address: ");
                                        con.OfficeAddress = Reusable.ReadLimited(50);
                                        contactInfor.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().OfficeAddress = con.OfficeAddress;
                                        break;
                                    case "4":
                                        Reusable.UpdateInfo("Enter new Office Phone: ");
                                        con.OfficePhone = Reusable.GetInputWholeNum(11);
                                        contactInfor.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().OfficePhone = con.OfficePhone;
                                        break;
                                    case "5":
                                        Reusable.UpdateInfo("Enter new Email Address: ");
                                        con.EmailAdd = Reusable.ReadLimited(30);
                                        contactInfor.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().EmailAdd = con.EmailAdd;
                                        break;
                                    case "6":
                                        Reusable.UpdateInfo("Enter new Cellphone Number: ");
                                        con.CellphoneNumber = Reusable.GetInputWholeNum(11);
                                        contactInfor.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().CellphoneNumber = con.CellphoneNumber;
                                        break;
                                    default:
                                        Reusable.InvalidInput("Choice is Invalid");
                                        break;
                                }
                                break;

                            case "3":
                                string specToUpdate;                             
                                specToUpdate = UpdateSpecMenu();
                                switch (specToUpdate)
                                {
                                    case "1":
                                        Reusable.UpdateInfo("Enter new Name: ");
                                        spec.Name = Reusable.ReadLimited(20);
                                        specializations.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().Name = spec.Name;
                                        break;
                                    case "2":
                                        Reusable.UpdateInfo("Enter new Description: ");
                                        spec.Description = Reusable.ReadLimited(50);
                                        specializations.Where(item => item.PhysicianId == forUpdate).FirstOrDefault().Description = spec.Description;
                                        break;
                                    default:
                                        Reusable.InvalidInput("Choice is Invalid");
                                        break;
                                }
                                break;
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine(@"Do you want to continue update? Y\N");
                        char choice = Console.ReadKey().KeyChar;
                        switch (Char.ToUpper(choice))
                        {
                            case 'Y':
                                UpdatePhysician();
                                break;
                            case 'N':
                                Main();
                                break;
                            default:
                                Reusable.InvalidInput("Choice is Invalid");
                                Main();
                                break;

                        }


                    }




                    else
                    {

                        Reusable.InvalidInput("\nPhysician id does not exist");
                    }

                    update = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong");
                }
        }

        private static string UpdatePerInfoMenu()
        {
            string itemToUpdate;
            Console.WriteLine("\n What do you want to edit? ");
            Console.WriteLine("=====================================================");
            Console.WriteLine(" 1. First name");
            Console.WriteLine(" 2. Middle name");
            Console.WriteLine(" 3. Last name");
            Console.WriteLine(" 4. Birth date");
            Console.WriteLine(" 5. Gender");
            Console.WriteLine(" 6. Height");
            Console.WriteLine(" 7. Weight");
            Console.Write("Please select an option from the menu: ");
            itemToUpdate = Console.ReadLine();
            return itemToUpdate;
        }

        private static string UpdateConInfoMenu()
        {
            string contactToUpdate;
            Console.WriteLine("\n What do you want to edit? ");
            Console.WriteLine("=====================================================");
            Console.WriteLine(" 1. Home Address");
            Console.WriteLine(" 2. Home Phone");
            Console.WriteLine(" 3. Office Address");
            Console.WriteLine(" 4. Office Phone");
            Console.WriteLine(" 5. Email Address");
            Console.WriteLine(" 6. Cellphone Number");
            Console.Write("Please select an option from the menu: ");
            contactToUpdate = Console.ReadLine();
            return contactToUpdate;
        }

        private static string UpdateSpecMenu()
        {
            string specToUpdate;
            Console.WriteLine("\n What do you want to edit? ");
            Console.WriteLine("=====================================================");
            Console.WriteLine(" 1. Name");
            Console.WriteLine(" 2. Description");
            Console.Write("Please select an option from the menu: ");
            specToUpdate = Console.ReadLine();
            return specToUpdate;
        }




        private static void AddPhysician()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("_____________________________________________________________________");
                Console.WriteLine("                            Add Physician                            ");
                Console.WriteLine("_____________________________________________________________________");
                Physician input = new Physician();
                ContactInfo contact = new ContactInfo();
                Specialization special = new Specialization();
                if (physicians.Count > 0)
                {
                    input.Id = physicians.Last().Id + 1;
                }
                else
                {
                    input.Id = 1;
                }
                Console.WriteLine();
                Console.WriteLine("===========================Personal Information======================");
                Console.Write("First Name (20 characters): ");
                input.FirstName = Reusable.ReadLimited(20);
                Console.Write("\nMiddle Name (20 characters): ");
                input.MiddleName = Reusable.ReadLimited(20);
                Console.Write("\nLast Name (20 characters): ");
                input.LastName = Reusable.ReadLimited(20);
                Console.Write("\nBirth Date: ");
                bool birthdate = false;
                while (!birthdate)
                {
                    try
                    {

                        input.BirthDate = Convert.ToDateTime(Console.ReadLine());
                        birthdate = true;
                    }
                    catch (Exception)
                    {
                        Reusable.InvalidInput("Please provide a valid date format: ");
                    }
                }
                Console.Write("Gender (M/F): ");
                input.Gender = Reusable.GetInputWithFormat("[FMfm]");
                Console.Write("Weight: ");
                input.Weight = Reusable.GetInputWithDec(4);
                Console.Write("\nHeight: ");
                input.Height = Reusable.GetInputWithDec(4);
                Console.WriteLine();
                Console.WriteLine("============================Contact Information======================");
                Console.Write("Home address (50 characters): ");
                contact.HomeAddress = Reusable.ReadLimited(50);
                Console.Write("\nHome phone (11 Characters): ");
                contact.HomePhone = Reusable.GetInputWholeNum(11);
                Console.Write("\nOffice Address (50 characters): ");
                contact.OfficeAddress = Reusable.ReadLimited(50);
                Console.Write("\nOffice Phone (11 Characters): ");
                contact.OfficePhone = Reusable.GetInputWholeNum(11);
                Console.Write("\nEmail Address (30 Characters): ");
                contact.EmailAdd = Reusable.ReadLimited(30);
                Console.Write("\nCellphone Number (11 Characters): ");
                contact.CellphoneNumber = Reusable.GetInputWholeNum(11);
                Console.WriteLine();
                Console.WriteLine("=============================Specialization==========================");
                Console.Write("Specialization: ");
                special.Name = Reusable.ReadLimited(20);
                Console.Write("\nDescription: ");
                special.Description = Reusable.ReadLimited(50);
                AddRecord(input);
                AddContactInfo(contact, input);
                AddSpecialization(special, input);
                Console.WriteLine("\n");
                Console.WriteLine(@"Do you want to add more Physician? Y\N");
                char choice = Console.ReadKey().KeyChar;
                switch (Char.ToUpper(choice))
                {
                    case 'Y':
                        AddPhysician();
                        break;
                    case 'N':
                        Main();
                        break;
                    default:
                        Reusable.InvalidInput("Choice is Invalid");
                        Main();
                        break;

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }



        }
        public static void AddRecord(Physician physician)

        {

            Physician phys = new Physician();

            phys.Id = physician.Id;
            phys.FirstName = physician.FirstName;
            phys.MiddleName = physician.MiddleName;
            phys.LastName = physician.LastName;
            phys.BirthDate = physician.BirthDate;
            phys.Gender = physician.Gender;
            phys.Height = physician.Height;
            phys.Weight = physician.Weight;

            physicians.Add(phys);

        }


        public static void AddContactInfo(ContactInfo contactInfo, Physician physician)
        {
            ContactInfo con = new ContactInfo();

            con.PhysicianId = physician.Id;
            con.HomeAddress = contactInfo.HomeAddress;
            con.HomePhone = contactInfo.HomePhone;
            con.OfficeAddress = contactInfo.OfficeAddress;
            con.OfficePhone = contactInfo.OfficePhone;
            con.EmailAdd = contactInfo.EmailAdd;
            con.CellphoneNumber = contactInfo.CellphoneNumber;

            contactInfor.Add(con);
        }

        public static void AddSpecialization(Specialization specialization, Physician physician)
        {
            Specialization spec = new Specialization();

            spec.PhysicianId = physician.Id;
            spec.Name = specialization.Name;
            spec.Description = specialization.Description;

            specializations.Add(spec);
        }

        private static void ViewAllPhysician()
        {
            try
            {

                Console.WriteLine("\n What do you want to view? ");
                Console.WriteLine("=====================================================");
                Console.WriteLine(" 1. Personal Information");
                Console.WriteLine(" 2. Contact Information");
                Console.WriteLine(" 3. Specialization");
                Console.Write("\n Please select an option from the menu: ");
                var itemToView = Console.ReadLine();
                switch (itemToView)
                {
                    case "1":
                        ViewPersonalInformation(physicians);
                        break;


                    case "2":
                        ViewContactInformation(contactInfor);
                        break;

                    case "3":
                        ViewSpecialization(specializations);
                        break;

                    default:
                        Reusable.InvalidInput("Choice is Invalid");
                        break;
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Something went wrong");
            }

        }

        private static void ViewPersonalInformation(List<Physician> list)
        {
            if (list.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("# Personal Information #");
                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                Console.WriteLine("Id    FirstName                MiddleName               LastName                 BirthDate      Gender  Weight   Height                                                                               ");
                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                foreach (var item in list)
                {

                    Console.Write("{0,-6}", item.Id);

                    Console.Write("{0,-25}", item.FirstName);

                    Console.Write("{0,-25}", item.MiddleName);

                    Console.Write("{0,-25}", item.LastName);

                    Console.Write("{0,-15}", item.BirthDate.ToShortDateString());

                    Console.Write("{0,-8}", item.Gender);

                    Console.Write("{0,-9}", item.Weight);

                    Console.Write("{0,-9}", item.Height);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("=========================================================================No Physician found! (Personal Information Unavailable)==============================================================================================");
                Console.ReadLine();
            }
        }




        private static void ViewContactInformation(List<ContactInfo> list)
        {
            if (list.Count > 0)
            {

                Console.WriteLine();
                Console.WriteLine("# Contact Information #");
                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                Console.WriteLine("Id    HomeAddress                                        HomePhone       OfficeAddress                                          OfficePhone     EmailAddress                       CellphoneNumber");
                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                foreach (var val in list)

                {
                    Console.Write("{0,-6}", val.PhysicianId);

                    Console.Write("{0,-51}", val.HomeAddress);

                    Console.Write("{0,-16}", val.HomePhone);

                    Console.Write("{0,-55}", val.OfficeAddress);

                    Console.Write("{0,-16}", val.OfficePhone);

                    Console.Write("{0,-35}", val.EmailAdd);

                    Console.Write("{0,-16}", val.CellphoneNumber);

                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("=========================================================================No Physician found! (Contact Information Unavailable)===============================================================================================");
                Console.ReadLine();
            }
        }


        private static void ViewSpecialization(List<Specialization> list)
        {
            if (list.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("# Specialization #");
                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                Console.WriteLine("Id    Name                     Description                                                                                                                                                            ");
                Console.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________");
                foreach (var spec in list)

                {
                    Console.Write("{0,-6}", spec.PhysicianId);
                    Console.Write("{0,-25}", spec.Name);
                    Console.Write("{0,-55}", spec.Description);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("=========================================================================No Physician found! (Specialization Unavailable)====================================================================================================");
                //Console.ReadLine();
            }
        }


        private static void GetPhysicianById()

        {
            

            try
            {

                Console.Write("Enter Physician Id: ");
                ulong id = Reusable.GetInputWholeNum(20);
                List<Physician> physicians1 = new List<Physician>();
                physicians1 = physicians.Where(item => item.Id == id).ToList();
                List<ContactInfo> contactInfor1 = new List<ContactInfo>();
                contactInfor1 = contactInfor.Where(item => item.PhysicianId == id).ToList();
                List<Specialization> specializations1 = new List<Specialization>();
                specializations1 = specializations.Where(item => item.PhysicianId == id).ToList();




                if (physicians.Count > 0)
                {

                    ViewPersonalInformation(physicians1);
                    Console.WriteLine();
                    ViewContactInformation(contactInfor1);
                    Console.WriteLine();
                    ViewSpecialization(specializations1);
                }
                else
                {
                    Console.WriteLine();
                    ViewPersonalInformation(physicians1);
                    Console.WriteLine();
                    ViewContactInformation(contactInfor1);
                    Console.WriteLine();
                    ViewSpecialization(specializations1);

                }


            }


            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }
        }




        public class Physician
        {

            public ulong Id { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Gender { get; set; }
            public double Weight { get; set; }
            public double Height { get; set; }
            public List<ContactInfo> ContactInfo { get; set; }
            public List<Specialization> Specialization { get; set; }

        }
        public class ContactInfo
        {
            public ulong PhysicianId { get; set; }
            public string HomeAddress { get; set; }
            public ulong HomePhone { get; set; }
            public string OfficeAddress { get; set; }
            public ulong OfficePhone { get; set; }
            public string EmailAdd { get; set; }
            public ulong CellphoneNumber { get; set; }

        }
        public class Specialization
        {
            public ulong PhysicianId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

        }


        public class Reusable
        {
            public static string ReadLimited(int limit)
            {
                string str = string.Empty;
                while (true)
                {
                    char c = Console.ReadKey(true).KeyChar;
                    if (c == '\r')

                        break;
                    if (c == '\b')
                    {
                        if (str != "")
                        {
                            str = str.Substring(0, str.Length - 1);
                            Console.Write("\b \b");
                        }
                    }
                    else if (str.Length < limit)
                    {
                        Console.Write(c);
                        str += c;

                    }

                }
                return str;

            }
            public static void InvalidInput(string errorMessage)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(errorMessage);
                Console.ResetColor();
            }

            public static void UpdateInfo(string updateMessage)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(updateMessage);
                Console.ResetColor();
            }

            public static double GetInputWithDec(int numberLimit)
            {

                string input = Reusable.ReadLimited(numberLimit);
                input = input.Trim();
                Regex regex = new Regex(@"[\d]{1,4}([.,][\d]{1,2})?");
                while (!regex.IsMatch(input))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("\nPlease Input Numerical value: ");
                    Console.ResetColor();
                    input = Console.ReadLine();

                }

                return Convert.ToDouble(input);

            }
            public static ulong GetInputWholeNum(int numberLimit)
            {

                string input = Reusable.ReadLimited(numberLimit);
                input = input.Trim();
                Regex regex = new Regex("^[0-9]+$");
                while (!regex.IsMatch(input))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("\nPlease Input Numerical value: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                }

                return System.Convert.ToUInt64(input);

            }

            public static string GetInputWithFormat(string validPattern = null)
            {
                var input = Console.ReadLine();
                input = input.Trim();
                while (validPattern != null && !Regex.IsMatch(input, validPattern))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Please Input Valid Format: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                }

                return input;
            }




        }



    }
}
