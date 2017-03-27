using PPOK_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PPOK_System.import
{
    public class Import
    {
        protected void csv(object sender)
        {
            //Find filepath in system
            using (var fs = File.OpenRead(@"C:\_git\PPOK_System\PPOK_System\Scrubbed_Data.csv"))
            {
                using (var reader = new StreamReader(fs))
                {
                    //read until file is ended
                    while (!reader.EndOfStream)
                    {

                        //Read in CSV file and create objects
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        Person p = new Person();
                        Prescription rx = new Prescription();
                        Drug d = new Drug();

                        //Import Person Information
                        p.person_id = Int32.Parse(values[0]);
                        p.first_name = values[1];
                        p.last_name = values[2];
                        p.date_of_birth = DateTime.Parse(values[3]);
                        p.email = values[6];

                        //Import Prescription Information
                        rx.date_filled = DateTime.Parse(values[7]);
                        rx.rx_id = Int32.Parse(values[8]);
                        rx.days_supply = Int32.Parse(values[9]);
                        rx.number_refills = Int32.Parse(values[10]);

                        //Import drug information
                        d.drug_name = values[12];

                    }
                }
            }
        }
    }
}