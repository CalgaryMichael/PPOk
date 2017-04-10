﻿using PPOK_System.Service;
using System.Web.Mvc;
using PPOK_System.TwilioManager;

namespace PPOK_System.Controllers {
	public class DatabaseController : Controller {
		Database db = new Database();
		

		// GET: Database
		public ActionResult Index() {
            db.initDatabase();
			//TwManager tw = new TwManager();
			//tw.ScheduleSend();
			return RedirectToAction("Index", "Home");
		}


		// GET: Database/TestStore
		public ActionResult TestStore() {
			//var store = db.ReadAllStore();
			var store = db.ReadSingleStore(1);
			return View(store);
		}


		// GET: Database/TestPerson
		public ActionResult TestPerson() {
			var person = db.ReadAllPersons();
			//var person = db.ReadSinglePerson(1);
			return View(person);
		}


		// GET: Database/TestDrug
		public ActionResult TestDrug() {
			//var drugs = db.ReadAllDrugs();
			var drugs = db.ReadSingleDrug("60505006501");
			return View(drugs);
		}


		// GET: Database/TestPrescription
		public ActionResult TestPrescription() {
			//var rx = db.ReadAllPrescriptions();
			//var rx = db.ReadSinglePrescription(1);
			var rx = db.ReadAllPrescriptionsForPerson(5);
			return View(rx);
		}


		// GET: Database/TestMessage
		public ActionResult TestMessage() {
			//var m = db.ReadAllMessages();
			var m = db.ReadSingleMessage(1);
			return View(m);
		}
	}
}