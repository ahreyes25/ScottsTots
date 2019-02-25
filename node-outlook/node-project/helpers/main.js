function calendarData(req) {

	var dayNum = { SUN: 0, MON: 1, TUES: 2, WED: 3, THURS: 4, FRI: 5, SAT: 6 };
	var monthLength = { JAN: 31, FEB: 28, MAR: 31, APR: 30, MAY: 31, JUN: 30, JUL: 31, AUG: 31, SEP: 30, OCT: 31, NOV: 30, DEC: 31 };
	var monthLength_ly = { JAN: 31, FEB: 29, MAR: 31, APR: 30, MAY: 31, JUN: 30, JUL: 31, AUG: 31, SEP: 30, OCT: 31, NOV: 30, DEC: 31 };

	// Current Date and Time Info
	var currentDate = new Date();
	var currentDayOfWeek = currentDate.getDay();
	var currentDayOfMonth = currentDate.getDate();
	var currentMonth = currentDate.getMonth() + 1;
	var currentYear = currentDate.getFullYear();
	var currentTime	= currentDate.toLocaleTimeString('en-US', { hour12: false, hour: "numeric", minute: "numeric" }) + ':00';
	var timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;

	// Get Reminders from TextBox
	var reminders = req.body.txtBx_reminders.split("\n");
	var usedReminders = [];
	var calendarEvents = [];






	// Monday Data
	if (req.body.chk_mon == 'on') {

		var monStartHour = req.body.num_mon_hour;
		var monStartMin	= req.body.num_mon_min;
		var monRepeat = req.body.num_mon_rep;

		// Get random reminder and store it appropriately
		var index = randomIndex(reminders);
		if (index > -1) {
	  		var reminder = reminders.splice(index, 1);
	  		var subject = reminder[0];
	  		var body = reminder[0];
	  		usedReminders.push(reminder);

	  		// Get new day of the week
	  		var dayDif = ((dayNum.MON + 7) - currentDayOfWeek) % 7;
	  		if (currentDayOfWeek + dayDif > 6)
	  			var newDay = currentDayOfWeek + dayDif - 7;
	  		else
	  			var newDay = currentDayOfWeek + dayDif;

	  		// Check if day wraps outside of the month
	  		switch(currentMonth) {
	  			case '01':
	  				var monthLen = monthLength.JAN;
	  				break;
	  			case '02':
	  				var monthLen = monthLength.FEB;
	  				break;
	  			case '03':
	  				var monthLen = monthLength.MAR;
	  				break;
	  			case '04':
	  				var monthLen = monthLength.APR;
	  				break;
	  			case '05':
	  				var monthLen = monthLength.MAY;
	  				break;
	  			case '06':
	  				var monthLen = monthLength.JUN;
	  				break;
	  			case '07':
	  				var monthLen = monthLength.JUL;
	  				break;
	  			case '08':
	  				var monthLen = monthLength.AUG;
	  				break;
	  			case '09':
	  				var monthLen = monthLength.SEP;
	  				break;
	  			case '10':
	  				var monthLen = monthLength.OCT;
	  				break;
	  			case '11':
	  				var monthLen = monthLength.NOV;
	  				break;
	  			case '12':
	  				var monthLen = monthLength.DEC;
	  				break;
	  		}

	  		var startDay = currentDayOfMonth + dayDif;
	  		var startMonth = currentMonth;
	  		var startYear = currentYear;

	  		// Wrap day around month
	  		if (currentDayOfMonth + dayDif > monthLen) {
	  			startDay = currentDayOfMonth + dayDif - monthLen;
	  			startMonth++;

	  			// Wrap month around year
	  			if (startMonth > 12) {
	  				startMonth = 1;
	  				startYear++;
	  			}
	  		}

	  		// Add 0 to beginning of date elements
			if (startDay < 10)
				startDay = '0' + startDay;
			if (startMonth < 10)
				startMonth = '0' + startMonth;

	  		// Create Start Time Var
	  		var startTime = startYear + "-" + startMonth + "-" + startDay + "T" + monStartHour + ":" + monStartMin + ":00";
			var endTime = startYear + "-" + startMonth + "-" + startDay + "T" + monStartHour + ":" + monStartMin + ":00";

			var event = {
				"subject": subject,
			    "body": {
			        "contentType": "HTML",
			        "content": body
			    },
			    "start": {
			        "dateTime": startTime,
			        "timeZone": timeZone
			    },
			    "end": {
			        "dateTime": endTime,
			        "timeZone": timeZone
			    }
			};

			console.log(event);

			calendarEvents.push(event);
		}
	}
	return calendarEvents;
}

exports.calendarData = calendarData;




function randomElement(array) {
	return array[ Math.floor(Math.random() * array.length) ];
}

function randomIndex(array) {
	return Math.floor(Math.random() * array.length);	
}




















