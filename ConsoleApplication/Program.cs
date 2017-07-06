using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //stops EF from going through its database initilization process when working with ninjacontext
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());

            //call the insertNinja method
            //InsertNinja();

            //call insert multiple ninjas method
            //InsertMultipleNinja();

            //Simple query
            //SimpleNinjaQueries();

            //QueryAndUpdateDisconnected();

            //InsertNinjaWithEquipment();

            Console.ReadKey();
        }

        #region Insert a Single Ninja
        private static void InsertNinja()
        {
            //Instantiate new ninja object and hydrate
            var ninja = new Ninja
            {
                Name = "DerekSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1990, 3, 2),
                ClanId = 1
            };
            //connect to out database context in a using statment to handle dispose
            using (var context = new NinjaContext())
            {
                //Log via console what the database is doing
                context.Database.Log = Console.WriteLine;
                //(Use .Add to insert a single object)Adding our hydrated object to out DBContext declaring what table to add it to
                //Format: CONTEXT.TABLE.COMMAND(PARAM)
                context.Ninjas.Add(ninja);
                //Tell context to save our changes
                context.SaveChanges();
            }
        }
        #endregion

        #region Insert Multiple Ninjas
        private static void InsertMultipleNinja()
        {
            //Instantiate new ninja object and hydrate
            var ninja1 = new Ninja
            {
                Name = "PeterSan",
                ServedInOniwaban = true,
                DateOfBirth = new DateTime(1989, 1, 20),
                ClanId = 1
            };
            //Instantiate second new ninja object and hydrate
            var ninja2 = new Ninja
            {
                Name = "ChrisSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1995, 8, 17),
                ClanId = 1
            };
            //connect to out database context in a using statment to handle dispose
            using (var context = new NinjaContext())
            {
                //Log via console what the database is doing
                context.Database.Log = Console.WriteLine;
                //.AddRange must be used to insert multiple records and accepts a list
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                //Tell context to save our changes
                context.SaveChanges();
            }
        }
        #endregion

        #region Insert Ninja With Equipment
        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja
                {
                    Name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };

                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                };

                var knife = new NinjaEquipment
                {
                    Name = "Knife",
                    Type = EquipmentType.Weapon
                };

                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(knife);
                context.SaveChanges();
            }
        }
        #endregion

        #region Simple Ninja Queries
        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                //simple syntax to return a list of ninjas from DB
                var ninjas = context.Ninjas.ToList();

                //Another way of executeing a simple query where you can declare the query first as a VAR then execute
                //This code block enumerates the query and executes each time
                //BE CAREFUL DB connection stays open while executing this query
                //var query = context.Ninjas;
                //foreach (var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}

                //Declare syntax where name = then query and return only those matched
                //var ninjas = context.Ninjas.Where(n => n.Name == "CraigSan");
                //foreach (var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}

                //.FirstOrDefault will find the first that matches result and return it, if no result will return null, can use instead of where BUT must be last call
                //var ninja = context.Ninjas.Where(n => n.Name == "CraigSan")
                //    .FirstOrDefault();
                //    Console.WriteLine(ninja.Name);

                //Can also implement paging
                //var ninja = context.Ninjas.
                //    Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1))
                //    .OrderBy(n => n.Name)
                //    .Skip(1).Take(1)
                //    .FirstOrDefault();
                //Console.WriteLine(ninja.Name);
            }
        }
        #endregion

        #region Query And Update
        private static void QueryAndUpdate()
        {
            //Declare context and using statment
            using (var context = new NinjaContext())
            {
                //for logging purposes to see what code is used
                context.Database.Log = Console.WriteLine;
                //retrieves a single ninja
                var ninja = context.Ninjas.FirstOrDefault();
                //edit the ninja
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                //save changes
                context.SaveChanges();
            }
        }
        #endregion

        #region Query And Update Disconnected
        private static void QueryAndUpdateDisconnected()
        {
            //Declare a disconnected ninja object
            Ninja ninja;

            //Declare context and using statment
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //finds the first ninja
                ninja = context.Ninjas.FirstOrDefault();
            }
            //modifies the ninja disconnected
            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //attach the modified ninja
                context.Ninjas.Attach(ninja); //Can Also Use .add
                //Tells EF to watch for the entry to be modified
                context.Entry(ninja).State = EntityState.Modified;
                //saves the changes
                context.SaveChanges();
            }
        }
        #endregion

        #region Retrieve Date With Find
        private static void RetrieveDataWithFind()
        {
            var keyval = 4;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //finds object by keyval
                var ninja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find 1: " + ninja.Name);
                
                //find will also check and see if entity is stored in memory first
                var someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find 2: " + someNinja.Name);
                ninja = null;

            }
        }
        #endregion

        #region Retrieve Date With Stored Proc
        private static void RetrieveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //uses stored proc to query and find ninjaName
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas"); //can also add .tolist()
                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name);
                }
            }
        }
        #endregion

        #region Delete Data with Find
        private static void DeleteDataWithFind()
        {
            var keyval = 4;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }
        #endregion

        #region Delete Data with Stored Proc
        private static void DeleteDataWithStoredProc()
        {
            var keyval = 4;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand("exec DeleteNinjaById {0}", keyval);
            }
        }
        #endregion

        #region Simple Ninja Graph Query
        private static void SimpleNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //EXAMPLE of EAGER LOADING
                //var ninja = context.Ninjas.Include(n => n.EquipmentOwned)
                //    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));

                //EXAMPLE OF EXPLICIT LOADING
                var ninja = context.Ninjas
                    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                context.Entry(ninja).Collection(n => n.EquipmentOwned).Load();



                Console.WriteLine("Ninja Retrieved: " + ninja.Name);
            }
        }
        #endregion

        #region Projection Query
        private static void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = context.Ninjas
                    .Select(n => new { n.Name, n.DateOfBirth, n.EquipmentOwned })
                    .ToList();
            }
        }
        #endregion
    }
}
