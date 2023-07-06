using ProductionDatabaseConnection.Scripts;
using ProductionDatabaseConnection.Scripts.Config;
using ProductionDatabaseConnection.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_monitoring_tool
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           //var prodContext = ProductionDatabaseFactory.BuildDatabaseConnection(DatabaseConfig.Backup);


           // var units = prodContext.units
           //            .Where(unit => unit.serial_num == "00000000")
           //            .Where(unit => unit.unit_series_id == "peregrine")
           //            .First();

           // Console.WriteLine(units.serial_num);




            //var jointUnitAndTestList = prodContext.unit_tests
            //   .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id,
            //     (unit_tests, test) => new { unit_tests, test })
            //   .Join(prodContext.units, jUnitTests => jUnitTests.unit_tests.unit_id, unit => unit.id,
            //     (jUnitTests, unit) => new { jUnitTests.test, unit })
            //   .Where(ob => ob.test.test_type_id == "peregrine_dive" && ob.test.passed)
            //   .OrderByDescending(ob => ob.test.created)
            //   .Take(50)
            //   .ToList();

            //foreach (var jointUnitAndTest in jointUnitAndTestList)
            //{
            //    var product = jointUnitAndTest.unit.unit_series_id;
            //    var snumber = jointUnitAndTest.unit.serial_num;
            //    var testPassed = jointUnitAndTest.test.passed;
            //    Console.WriteLine($"{product} {snumber}: Passed = {testPassed}");

            //}





            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
