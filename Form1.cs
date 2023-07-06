using ProductionDatabaseConnection.Scripts.Config;
using ProductionDatabaseConnection.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.Entity.Infrastructure;
using Org.BouncyCastle.Asn1.Cms;
using ProductionDatabaseConnection.Scripts.Model;




namespace Production_monitoring_tool
{
    public partial class Form1 : Form
    {

        


        // Model class to hold the data
        private class MyModel
        {
            public long UnitId { get; set; }
            public string Product { get; set; }
            public string SerialNumber { get; set; }
            public bool TestPassed { get; set; }
            public DateTime TestTime { get; set; }

        }
        //private List<MyModel> PreviousData { get; set; } // This is just for testing 
        //public static Form1 instance;


        public List<long> excludedUnitIds;
        public Form1()
        {
            InitializeComponent();
            perviousDayData();
            GetExcludedUnitIds();
            excludedUnitIds = GetExcludedUnitIds();///%%%%%%%%%%%%testing the apparch of having teh table outside 
            //SomeFunction();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TestdataGridView.DataSource = PreviousData; // This is just for testing 



            //Auto Size 
            orignalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            controlDictionary = new Dictionary<Control, (Rectangle, float)>
            {

                //Title Panel
                { panel1, (panel1.Bounds, panel1.Font.Size) },
                { label1, (label1.Bounds, label1.Font.Size) },
                { dateToday, (dateToday.Bounds, dateToday.Font.Size) },
                { btnClose, (btnClose.Bounds, btnClose.Font.Size) },
                { versionlabel, (versionlabel.Bounds, versionlabel.Font.Size) },
                { buttonOpenFoam2, (buttonOpenFoam2.Bounds, buttonOpenFoam2.Font.Size) },


                //Peregrine Panel   
                { peregrinepanel, (peregrinepanel.Bounds, peregrinepanel.Font.Size) },
                { peregrine_label, (peregrine_label.Bounds, peregrine_label.Font.Size) },
                { peregrine_active_label, (peregrine_active_label.Bounds, peregrine_active_label.Font.Size) },
                { peregrine_first_time, (peregrine_first_time.Bounds, peregrine_first_time.Font.Size) },
                { Num_peregrine_post, (Num_peregrine_post.Bounds, Num_peregrine_post.Font.Size) },
                { peregrine_yesterday, (peregrine_yesterday.Bounds, peregrine_yesterday.Font.Size) },
                { peregrine_goal, (peregrine_goal.Bounds, peregrine_goal.Font.Size) },
                { pictureBox6, (pictureBox6.Bounds, pictureBox6.Font.Size) },

                //Petrel 3 Panel 
                { petrel3panel, (petrel3panel.Bounds, petrel3panel.Font.Size) },
                { petrel3_label, (petrel3_label.Bounds, petrel3_label.Font.Size) },
                { petrel3_active_label, (petrel3_active_label.Bounds, petrel3_active_label.Font.Size) },
                { petrel3_first_time, (petrel3_first_time.Bounds, petrel3_first_time.Font.Size) },
                { num_petrel3_dive, (num_petrel3_dive.Bounds, num_petrel3_dive.Font.Size) },
                { petrel3_yesterday, (petrel3_yesterday.Bounds, petrel3_yesterday.Font.Size) },
                { petrel3_goal, (petrel3_goal.Bounds, petrel3_goal.Font.Size) },
                { pictureBox5, (pictureBox5.Bounds, pictureBox5.Font.Size) },

                //Petrel 3 Panel
                { perdix2panel, (perdix2panel.Bounds, perdix2panel.Font.Size) },
                { perdix2_label, (perdix2_label.Bounds, perdix2_label.Font.Size) },
                { perdix2_active_label, (perdix2_active_label.Bounds, perdix2_active_label.Font.Size) },
                { pedix2_first_time, (pedix2_first_time.Bounds, pedix2_first_time.Font.Size) },
                { num_perdix2_post, (num_perdix2_post.Bounds, num_perdix2_post.Font.Size) },
                { perdix2_yesterday, (perdix2_yesterday.Bounds, perdix2_yesterday.Font.Size) },
                { perdix2_goal, (perdix2_goal.Bounds, perdix2_goal.Font.Size) },
                { pictureBox1, (pictureBox1.Bounds, pictureBox1.Font.Size) },

                //Swift Panel
                { swiftpanel, (swiftpanel.Bounds, swiftpanel.Font.Size) },
                { swift_label, (swift_label.Bounds, swift_label.Font.Size) },
                { swift_active_label, (swift_active_label.Bounds, swift_active_label.Font.Size) },
                { swift_first_time, (swift_first_time.Bounds, swift_first_time.Font.Size) },
                { num_swift_post, (num_swift_post.Bounds, num_swift_post.Font.Size) },
                { swift_yesterday, (swift_yesterday.Bounds, swift_yesterday.Font.Size) },
                { swift_goal, (swift_goal.Bounds, swift_goal.Font.Size) },
                { pictureBox2, (pictureBox2.Bounds, pictureBox2.Font.Size) },

                ////Teirc Panel
                { tericpanel, (tericpanel.Bounds, tericpanel.Font.Size) },
                { teric_label, (perdix2_label.Bounds, perdix2_label.Font.Size) },
                { teric_active_label, (perdix2_active_label.Bounds, perdix2_active_label.Font.Size) },
                { teric_first_time, (pedix2_first_time.Bounds, pedix2_first_time.Font.Size) },
                { num_teric_post, (num_perdix2_post.Bounds, num_perdix2_post.Font.Size) },
                { teric_yesterday, (perdix2_yesterday.Bounds, perdix2_yesterday.Font.Size) },
                { teric_goal, (perdix2_goal.Bounds, perdix2_goal.Font.Size) },
                { pictureBox3, (pictureBox3.Bounds, pictureBox3.Font.Size) },

                ////Nerd 2 Panel
                { nerd2panel, (nerd2panel.Bounds, nerd2panel.Font.Size) },
                { nerd2_label, (perdix2_label.Bounds, perdix2_label.Font.Size) },
                { nerd2_active_label, (perdix2_active_label.Bounds, perdix2_active_label.Font.Size) },
                { nerd2_first_time, (pedix2_first_time.Bounds, pedix2_first_time.Font.Size) },
                { num_nerd2_post, (num_perdix2_post.Bounds, num_perdix2_post.Font.Size) },
                { nerd2_yesterday, (perdix2_yesterday.Bounds, perdix2_yesterday.Font.Size) },
                { nerd2_goal, (perdix2_goal.Bounds, perdix2_goal.Font.Size) },
                { pictureBox4, (pictureBox4.Bounds, pictureBox4.Font.Size) }
            };
            

        }

        ///%%%%%%%%%%%%testing the apparch of having teh table outside 

        public List<long> GetExcludedUnitIds()
        {
            var prodContext = ProductionDatabaseFactory.BuildDatabaseConnection(DatabaseConfig.UnitManager);

            // Get the current date and time
            DateTime currentDate = DateTime.Now;

            dateToday.Text = currentDate.ToString("dddd, dd MMMM yyyy");

            // Define the start time and end time for the time range
            DateTime startTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 7, 0, 0); // Start time 7:00 AM
            DateTime endTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 18, 0, 0); //  Stop time 4:00 PM

            var excludedUnitIds = prodContext.unit_tests
                .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
                .Where(innerOb => innerOb.test.passed && (
                    innerOb.test.test_type_id == "peregrine_dive" ||
                    innerOb.test.test_type_id == "petrel_3_dive" ||
                    innerOb.test.test_type_id == "perdix_2_dive" ||
                    innerOb.test.test_type_id == "swift_dive" ||
                    innerOb.test.test_type_id == "teric_dive" ||
                    innerOb.test.test_type_id == "nerd_2_dive"))
                .Where(innerOb => innerOb.test.modified < startTime)
                .Select(innerOb => innerOb.unit_tests.unit_id)
                .Distinct()
                .ToList();

            return excludedUnitIds;
        }

        ///^^^^^^^^testing the apparch of having teh table outside 



        /// Below is Section of the code for previous day data
        private List<MyModel> GetDataForPreviousDay(string tableName, List<JoinUnitTest> joinUnitTestList)
        {
            var dataList = joinUnitTestList
                .Where(ob => ob.test.test_type_id == tableName && ob.test.passed) 
                // the code below is for filtering RMA unit and repeated tested unit
                .GroupBy(ob => ob.unit_tests.unit_id)
                .SelectMany(g => g.Take(1))
                // the code above is for filtering RMA unit and repeated tested unit
                .OrderByDescending(ob => ob.test.modified)
                .Select(jointUnitAndTest => new MyModel
                {
                    Product = jointUnitAndTest.unit.unit_series_id,
                    SerialNumber = jointUnitAndTest.unit.serial_num,
                    TestPassed = jointUnitAndTest.test.passed,
                    TestTime = jointUnitAndTest.test.modified
                })
                .ToList();

            return dataList;
        }



        ///*************** the code below is just for testing  just for testing *******************//////////////////////////
        //private void SomeFunction()
        //{
        //    // Assign the return value of the previous function to the public variable
        //    PreviousData = previous();
        //}

        //private List<MyModel> previous()
        //{
        //    var prodContext = ProductionDatabaseFactory.BuildDatabaseConnection(DatabaseConfig.UnitManager);

        //    // Get the current date and time
        //    DateTime currentDate = DateTime.Now;

        //    dateToday.Text = currentDate.ToString("dddd, dd MMMM yyyy");

        //    // Define the start time and end time for the time range
        //    DateTime startTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 7, 0, 0); // Start time 7:00 AM
        //    DateTime endTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 18, 0, 0); //  Stop time 4:00 PM


        //    var rowsBeforeSpecifiedDay = prodContext.unit_tests
        //        .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
        //        .Join(prodContext.units, jUnitTests => jUnitTests.unit_tests.unit_id, unit => unit.id, (jUnitTests, unit) => new JoinUnitTest()
        //        {
        //            test = jUnitTests.test,
        //            unit_tests = jUnitTests.unit_tests,
        //            unit = unit
        //        })
        //        .Where(ob => ob.test.created >= startTime && ob.test.test_type_id == "peregrine_dive" && ob.test.created <= endTime)
        //        .Where(ob => ob.test.passed && !prodContext.unit_tests
        //            .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
        //            .Where(innerOb => innerOb.test.passed && innerOb.test.test_type_id == "peregrine_dive" && innerOb.test.created < startTime)
        //            .Select(innerOb => innerOb.unit_tests.unit_id)
        //            .Distinct()
        //            .Contains(ob.unit_tests.unit_id)

        //            )
        //        .OrderByDescending(ob => ob.test.created)
        //       .Select(jointUnitAndTest => new MyModel
        //       {
        //           Product = jointUnitAndTest.unit.unit_series_id,
        //           SerialNumber = jointUnitAndTest.unit.serial_num,
        //           TestPassed = jointUnitAndTest.test.passed,
        //           TestTime = jointUnitAndTest.test.created,
        //           UnitId = jointUnitAndTest.unit_tests.unit_id
        //       })
        //       .ToList();


        //    //var rowsBeforeSpecifiedDay = prodContext.unit_tests
        //    //    .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id,
        //    //          (unit_tests, test) => new { unit_tests, test })
        //    //    .Join(prodContext.units, jUnitTests => jUnitTests.unit_tests.unit_id, unit => unit.id,
        //    //          (jUnitTests, unit) => new JoinUnitTest()
        //    //          {
        //    //              test = jUnitTests.test,
        //    //              unit_tests = jUnitTests.unit_tests,
        //    //              unit = unit
        //    //          })
        //    //    .Where(ob => ob.test.passed && ob.test.modified < new DateTime(2023, 05, 15, 0, 0, 0))
        //    //    .OrderByDescending(ob => ob.unit_tests.unit_id)
        //    //    .Select(jointUnitAndTest => new MyModel
        //    //    {
        //    //        Product = jointUnitAndTest.unit.unit_series_id,
        //    //        SerialNumber = jointUnitAndTest.unit.serial_num,
        //    //        TestPassed = jointUnitAndTest.test.passed,
        //    //        TestTime = jointUnitAndTest.test.created,
        //    //        UnitId = jointUnitAndTest.unit_tests.unit_id
        //    //    })
        //    //    .ToList();
        //    return rowsBeforeSpecifiedDay;
        //}

        ////********** The code above is just for testing*************** ///




        private void GetDataForDeviceForPreviousDay(string deviceName, List<JoinUnitTest> testList, DataGridView dataGridView, Label label)
        {
            var dataList = GetDataForPreviousDay(deviceName, testList);
            dataGridView.DataSource = dataList;
            label.Text = $"Yesterday units: {dataGridView.RowCount}"; 
        }

        private void perviousDayData()
        {
            var prodContext = ProductionDatabaseFactory.BuildDatabaseConnection(DatabaseConfig.UnitManager);

            // Get the current date and time
            DateTime currentDate = DateTime.Now;
            dateToday.Text = currentDate.ToString("dddd, dd MMMM yyyy");

            // Get the date of 24 hours ago
            DateTime previousDate = currentDate.AddDays(-1);
            DateTime startTime = new DateTime(previousDate.Year, previousDate.Month, previousDate.Day); // Start time at midnight
            DateTime endTime = startTime.AddDays(1).AddSeconds(-1); // End time at the end of the day

            var testTypeIds = new List<string>
                {
                    "peregrine_dive",
                    "petrel_3_dive",
                    "perdix_2_dive",
                    "swift_dive",
                    "teric_dive",
                    "nerd_2_dive"
                 };

            var jointUnitAndTestList = prodContext.unit_tests
                .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
                .Join(prodContext.units, jUnitTests => jUnitTests.unit_tests.unit_id, unit => unit.id, (jUnitTests, unit) => new JoinUnitTest()
                {
                    test = jUnitTests.test,
                    unit_tests = jUnitTests.unit_tests,
                    unit = unit
                })
                .Where(ob => ob.test.modified >= startTime && ob.test.modified <= endTime)
                .Where(ob => ob.test.passed && !prodContext.unit_tests
                    .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
                    .Where(innerOb => innerOb.test.passed && testTypeIds.Contains(innerOb.test.test_type_id) && innerOb.test.modified < startTime)
                    .Select(innerOb => innerOb.unit_tests.unit_id)
                    .Distinct()
                    .Contains(ob.unit_tests.unit_id))
                .OrderByDescending(ob => ob.test.modified)
                .ToList();

            ///// Product line 
            GetDataForDeviceForPreviousDay("peregrine_dive", jointUnitAndTestList, peregrineGridView, peregrine_yesterday);// Peregrine
            GetDataForDeviceForPreviousDay("petrel_3_dive", jointUnitAndTestList, petrel3dataGridView, petrel3_yesterday);// Petrel 3
            GetDataForDeviceForPreviousDay("perdix_2_dive", jointUnitAndTestList, Perdix2dataGridView, perdix2_yesterday);// Perdix 2
            GetDataForDeviceForPreviousDay("teric_dive", jointUnitAndTestList, TericdataGridView, teric_yesterday);// Teric
            GetDataForDeviceForPreviousDay("swift_dive", jointUnitAndTestList, SwiftdataGridView, swift_yesterday);// Swift
            GetDataForDeviceForPreviousDay("nerd_2_dive", jointUnitAndTestList, Nerd2dataGridView, nerd2_yesterday);// Nerd 2

        }
       
        private List<MyModel> data_collection(string tableName, Label textBox, List<JoinUnitTest> joinUnitofTestlist)
        {

            // Declare a DateTime variable to store the timestamp of the first data row received
            DateTime firstDataRowReceivedTime = DateTime.MinValue;
            var today = DateTime.Today;

            var jointUnitAndTestList = joinUnitofTestlist
                .Where(ob => ob.test.test_type_id == tableName && ob.test.passed) 
                 // the code below is for filtering RMA unit and repeated tested unit
                .GroupBy(ob => ob.unit_tests.unit_id)
                .Select(g => g.First()) //can be replaced with this .SelectMany(g => g.Take(1))
                // the code above is for filtering RMA unit and repeated tested unit
                .OrderByDescending(ob => ob.test.modified)
                .ToList();


            // Create a list of MyModel objects to hold the retrieved data
            List<MyModel> dataList = new List<MyModel>();

            foreach (var jointUnitAndTest in jointUnitAndTestList)
            {
                var product = jointUnitAndTest.unit.unit_series_id;
                var snumber = jointUnitAndTest.unit.serial_num;
                var testPassed = jointUnitAndTest.test.passed;
                var testtime = jointUnitAndTest.test.modified;
                var unitId = jointUnitAndTest.unit_tests.unit_id;


                // Add the retrieved data to the list
                dataList.Add(new MyModel
                {
                    Product = product,
                    SerialNumber = snumber,
                    TestPassed = testPassed,
                    TestTime = testtime,
                    UnitId = unitId
                });


                // Check if the current row's test time is earlier than the stored firstDataRowReceivedTime
                if (testtime < firstDataRowReceivedTime || firstDataRowReceivedTime == DateTime.MinValue)
                {
                    // Update the firstDataRowReceivedTime with the current row's test time
                    firstDataRowReceivedTime = testtime;
                    textBox.Text = "First Scan:" + firstDataRowReceivedTime.ToString("hh:mm tt");
                }
                else { textBox.Text = "First Scan: N/A"; }

            }

            return dataList;
        }

 

        private void GetDataForDevice(string deviceName, Label firstTime, List<JoinUnitTest> testList, DataGridView dataGridView, Label label)
        {
            var dataList = data_collection(deviceName, firstTime, testList);
            dataGridView.DataSource = dataList;
            label.Text = $"{dataGridView.RowCount} units";
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var prodContext = ProductionDatabaseFactory.BuildDatabaseConnection(DatabaseConfig.UnitManager);

                // Get the current date and time
                DateTime currentDate = DateTime.Now;

                dateToday.Text = currentDate.ToString("dddd, dd MMMM yyyy");

                // Define the start time and end time for the time range
                DateTime startTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 6, 0, 0); // Start time 7:00 AM
                DateTime endTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 18, 0, 0); //  Stop time 4:00 PM


                //var testTypeIds = new List<string>
                //{
                //    "peregrine_dive",
                //    "petrel_3_dive",
                //    "perdix_2_dive",
                //    "swift_dive",
                //    "teric_dive",
                //    "nerd_2_dive"
                // };

                //var jointUnitAndTestList = prodContext.unit_tests
                //    .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
                //    .Join(prodContext.units, jUnitTests => jUnitTests.unit_tests.unit_id, unit => unit.id, (jUnitTests, unit) => new JoinUnitTest()
                //    {
                //        test = jUnitTests.test,
                //        unit_tests = jUnitTests.unit_tests,
                //        unit = unit
                //    })
                //    .Where(ob => ob.test.modified >= startTime && ob.test.modified <= endTime)
                //    .Where(ob => ob.test.passed && !prodContext.unit_tests
                //        .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
                //        .Where(innerOb => innerOb.test.passed && testTypeIds.Contains(innerOb.test.test_type_id) && innerOb.test.modified < startTime)
                //        .Select(innerOb => innerOb.unit_tests.unit_id)
                //        .Distinct()
                //        .Contains(ob.unit_tests.unit_id))
                //    .OrderByDescending(ob => ob.test.modified)
                //    .ToList();


                // ---the apprach to have the ecluding fucntion outside 
                var jointUnitAndTestList = prodContext.unit_tests
                    .Join(prodContext.tests, unitTests => unitTests.test_id, test => test.id, (unit_tests, test) => new { unit_tests, test })
                    .Join(prodContext.units, jUnitTests => jUnitTests.unit_tests.unit_id, unit => unit.id, (jUnitTests, unit) => new JoinUnitTest()
                    {
                        test = jUnitTests.test,
                        unit_tests = jUnitTests.unit_tests,
                        unit = unit
                    })
                    .Where(ob => ob.test.modified >= startTime && ob.test.modified <= endTime && ob.test.passed)
                    .ToList()
                    .Where(ob => !excludedUnitIds.Contains(ob.unit_tests.unit_id))
                    .OrderByDescending(ob => ob.test.modified)
                    .ToList();


                ///// Product line 
                GetDataForDevice("peregrine_dive", peregrine_first_time, jointUnitAndTestList, peregrineGridView, Num_peregrine_post);// Peregrine
                GetDataForDevice("petrel_3_dive", petrel3_first_time, jointUnitAndTestList, petrel3dataGridView, num_petrel3_dive);// Petrel 3
                GetDataForDevice("perdix_2_dive", pedix2_first_time, jointUnitAndTestList, Perdix2dataGridView, num_perdix2_post);// Perdix 2
                GetDataForDevice("teric_dive", teric_first_time, jointUnitAndTestList, TericdataGridView, num_teric_post);// Teric
                GetDataForDevice("swift_dive", swift_first_time, jointUnitAndTestList, SwiftdataGridView, num_swift_post);// Swift
                GetDataForDevice("nerd_2_dive", nerd2_first_time, jointUnitAndTestList, Nerd2dataGridView, num_nerd2_post);// Nerd 2

                active_line();

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lost Netwrok Connection");
                var result = MessageBox.Show("       Lost Network Connection! \n \n Do you want to close the application?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Application.Exit(); // Close the application
                }
                else if (result == DialogResult.No)
                {
                    // Perform any necessary actions to fix the issue here
                    // Then restart the application
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                }
            }
        }
        

        private void active_line()
        {
            // Update the panel for all the product assembly line
            var controls = new[] {
                new { DataGridView = peregrineGridView, Panel = peregrinepanel, Label = peregrine_active_label  },// Peregrine
                new { DataGridView = petrel3dataGridView, Panel = petrel3panel, Label = petrel3_active_label },// Petrel3
                new { DataGridView = Perdix2dataGridView, Panel = perdix2panel, Label = perdix2_active_label },// Perdix 2
                new { DataGridView = TericdataGridView, Panel = tericpanel, Label = teric_active_label },// Teric 
                new { DataGridView = SwiftdataGridView, Panel = swiftpanel, Label = swift_active_label },// Swift 
                new { DataGridView = Nerd2dataGridView, Panel = nerd2panel, Label = nerd2_active_label },// Nerd 2
             };

            //Form2 form2 = new Form2();
            //form2.ShowDialog();

            //string peregrineGoal = form2.peregrineGoal;
            //string petrel3Goal = form2.petrel3Goal;
            //string perdix2Goal = form2.perdix2Goal;
            //string swiftGoal = form2.swiftGoal;
            //string tericGoal = form2.tericGoal;
            //string nerd2Goal = form2.nerd2Goal;


            //var controls = new[] {
            //    new { DataGridView = peregrineGridView, Panel = peregrinepanel, Label1 = peregrine_active_label, Label2 = Num_peregrine_post, Label3 = peregrineGoal },// Peregrine
            //    new { DataGridView = petrel3dataGridView, Panel = petrel3panel, Label1 = petrel3_active_label, Label2 = num_petrel3_dive, Label3 = petrel3Goal },// Petrel3
            //    new { DataGridView = Perdix2dataGridView, Panel = perdix2panel, Label1 = perdix2_active_label, Label2 = num_perdix2_post, Label3 = perdix2Goal },// Perdix 2
            //    new { DataGridView = TericdataGridView, Panel = tericpanel, Label1 = teric_active_label, Label2 = num_swift_post, Label3 = tericGoal},// Teric 
            //    new { DataGridView = SwiftdataGridView, Panel = swiftpanel, Label1 = swift_active_label, Label2 = num_teric_post, Label3 = swiftGoal },// Swift 
            //    new { DataGridView = Nerd2dataGridView, Panel = nerd2panel, Label1 = nerd2_active_label, Label2 = num_nerd2_post, Label3 = nerd2Goal },// Nerd 2
            //};

            foreach (var control in controls)
            {
                if (control.DataGridView.Rows.Count > 0)
                {
                    control.Panel.BackColor = Color.NavajoWhite;
                    control.Label.Text = "Active";
                    control.Label.ForeColor = Color.DarkGreen;
                    control.Panel.BorderStyle = BorderStyle.Fixed3D;


                }
                else
                {
                    control.Panel.BackColor = Color.AliceBlue;
                    control.Label.Text = "Inactive";
                    control.Label.ForeColor = Color.Red;
                    control.Panel.BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOpenFoam2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

            string peregrineGoal = form2.peregrineGoal;
            peregrine_goal.Text = "Goal of today:" + peregrineGoal;

            string petrel3Goal = form2.petrel3Goal;
            petrel3_goal.Text = "Goal of today:" + petrel3Goal;

            string perdix2Goal = form2.perdix2Goal;
            perdix2_goal.Text = "Goal of today:" + perdix2Goal;

            string swiftGoal = form2.swiftGoal;
            swift_goal.Text = "Goal of today:" + swiftGoal;

            string tericGoal = form2.tericGoal;
            teric_goal.Text = "Goal of today:" + tericGoal;

            string nerd2Goal = form2.nerd2Goal;
            nerd2_goal.Text = "Goal of today:" + nerd2Goal; 
        }

       
        private void previousDayTimer_Tick(object sender, EventArgs e)
        {
            // Run your function here
            perviousDayData();
            GetExcludedUnitIds();
            excludedUnitIds = GetExcludedUnitIds();
        }


        // Auto Size
        private Rectangle orignalFormSize;
        private Dictionary<Control, (Rectangle, float)> controlDictionary;

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeChildrenControls();
        }
        
        private void ResizeChildrenControls()
        {
            foreach (var controlEntry in controlDictionary)
            {
                Control control = controlEntry.Key;
                Rectangle originalRect = controlEntry.Value.Item1;
                float originalFontSize = controlEntry.Value.Item2;

                float xRatio = (float)this.Width / (float)orignalFormSize.Width;
                float yRatio = (float)this.Height / (float)orignalFormSize.Height;

                int newX = (int)(originalRect.X * xRatio);
                int newY = (int)(originalRect.Y * yRatio);
                int newWidth = (int)(originalRect.Width * xRatio);
                int newHeight = (int)(originalRect.Height * yRatio);

                control.SetBounds(newX, newY, newWidth, newHeight);

                float ratio = Math.Min(xRatio, yRatio);
                float newFontSize = originalFontSize * ratio;
                control.Font = new Font(control.Font.FontFamily, newFontSize);
            }
        }

        
    }

    internal class JoinUnitTest
    {
        public test test;
        public unit_tests unit_tests;
        public unit unit;
    }
}
