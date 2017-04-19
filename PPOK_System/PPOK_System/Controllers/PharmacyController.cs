﻿using PPOK_System.Models;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace PPOK_System.Controllers {
    public class PharmacyController : BaseController {
		Database db = new Database(SystemContext.DefaultConnectionString);

        // GET: Pharmacy
        public ActionResult Index() {
			var p = db.ReadAllMessages();
            return View(p);
        }


		// GET: /Pharmacy/EditCustomer/{id}
        public ActionResult EditCustomer(int id) {
			var p = db.ReadSinglePerson(id);
            return PartialView(p);
        }


		// POST: /Pharmacy/EditPharmacy/{store}
		[HttpPost]
		public ActionResult EditCustomer(Person p) {
			db.Update(p);
			return RedirectToAction("manageCustomer", "Pharmacy");
		}


        public ActionResult Recall()
        {
            return View();
        }


        public ActionResult History()
        {
            return View();
        }


		// GET: /Pharmacy/PersonHistory/
        public ActionResult manageCustomer() {
			var p = db.ReadAllPersons();
            return View(p);
        }
		

		// POST: /Pharmacy/PersonHistory/{id}
		[HttpPost]
		public ActionResult PersonHistory(int id) {
			var msg = db.ReadAllMessagesForPerson(id);
			return PartialView(msg);
		}

        public ActionResult AddPerson()
        {
            return PartialView();
        }



        [HttpPost]
        public ActionResult AddPerson2(Person p)
        {
            List<Person> temp = new List<Person>();
            int numOfPeople = 0;
            PPOK_System.Domain.Service.Database db = new PPOK_System.Domain.Service.Database(PPOK_System.Models.SystemContext.DefaultConnectionString);

            temp = db.ReadAllPersons();
            numOfPeople = temp.Count;
            p.person_id = numOfPeople + 1;
            db.Create(p);
            return RedirectToAction("Index", "Pharmacy");
        }
    }
}