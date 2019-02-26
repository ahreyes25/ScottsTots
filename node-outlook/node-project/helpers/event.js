/*	--- TO DO ---
	[ ] Free & Private Time
	[ ] Leap Year
	[ ] Repeat

	[ ] Database Integration
	[ ] Delete Events
	[ ] Reset API and BD Private Keys

	[ ] Google Calendar Implementation
	[ ] iOS Calendar Implementation
	[ ] Android Calendar Implementation
*/

var dayNum = { SUN: 0, MON: 1, TUES: 2, WED: 3, THURS: 4, FRI: 5, SAT: 6 };
var monthLength = [ 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 ];
var monthLength_ly = [ 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 ];

var currentDate = new Date();
var currentDayOfWeek = currentDate.getDay();
var currentDayOfMonth = currentDate.getDate();
var currentMonth = currentDate.getMonth() + 1;
var currentYear = currentDate.getFullYear();
var currentTime	= currentDate.toLocaleTimeString('en-US', { hour12: false, hour: "numeric", minute: "numeric" }) + ':00';
var timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
var leapYear = false;

var usedReminders = [];
var calendarEvents = [];
var unusedReminders = [];

// Take in POST request and create calender events for each day
function calendarData(req) {
	// Make sure reminders box is not empty
	if (req.body.txtBx_reminders != "") {

		var reminders = req.body.txtBx_reminders.split("\n");
		unusedReminders = reminders;

		// Monday Data
		if (req.body.chk_mon == 'on')
			if (unusedReminders.length > 0)
				calendarEvents.push(createEvent(unusedReminders, dayNum.MON, req.body.num_mon_hour, req.body.num_mon_min));
		
		// Tuesday Data
		if (req.body.chk_tues == 'on')
			if (unusedReminders.length > 0)
				calendarEvents.push(createEvent(unusedReminders, dayNum.TUES, req.body.num_tues_hour, req.body.num_tues_min));
		
		// Wednesday Data
		if (req.body.chk_wed == 'on')
			if (unusedReminders.length > 0)
				calendarEvents.push(createEvent(unusedReminders, dayNum.WED, req.body.num_wed_hour, req.body.num_wed_min));

		// Thursday Data
		if (req.body.chk_thurs == 'on')
			if (unusedReminders.length > 0)
				calendarEvents.push(createEvent(unusedReminders, dayNum.THURS, req.body.num_thurs_hour, req.body.num_thurs_min));

		// Friday Data
		if (req.body.chk_fri == 'on')
			if (unusedReminders.length > 0)
				calendarEvents.push(createEvent(unusedReminders, dayNum.FRI, req.body.num_fri_hour, req.body.num_fri_min));

		// Saturday Data
		if (req.body.chk_sat == 'on')
			if (unusedReminders.length > 0)
				calendarEvents.push(createEvent(unusedReminders, dayNum.SAT, req.body.num_sat_hour, req.body.num_sat_min));

		// Sunday Data
		if (req.body.chk_sun == 'on')
			if (unusedReminders.length > 0)
				calendarEvents.push(createEvent(unusedReminders, dayNum.SUN, req.body.num_sun_hour, req.body.num_sun_min));

		return calendarEvents;
	}
	else {
		unusedReminders = [];
		usedReminders = [];
		calendarEvents = [];
	}
	return [];
}
exports.calendarData = calendarData;



function createEvent(reminders, dayNum, startHour, startMin) {

	// Get reminders
	if (reminders.length > 0) {

		var index = randomIndex(reminders);
		if (index > -1) {

	  		var reminder = reminders.splice(index, 1);
	  		var subject = reminder[0];
	  		var body = reminder[0];
	  		usedReminders.push(reminder);

			// Get new day of the week
			var dayDif = ((dayNum + 7) - currentDayOfWeek) % 7;
			if (currentDayOfWeek + dayDif > 6)
				var newDay = currentDayOfWeek + dayDif - 7;
			else
				var newDay = currentDayOfWeek + dayDif;

			// Get month length from array
			if (!leapYear)
				var monthLen = monthLength[currentMonth - 1];
			else
				var monthLen = monthLength_ly[currentMonth - 1];

			// Store starting values
			var startDay = currentDayOfMonth + dayDif;
			var startMonth = currentMonth;
			var startYear = currentYear;

			// Update starting values by checking for day/month/year wraps
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
			if (startHour < 10)
				startHour = '0' + startHour;

			// Create Start Time Var
			var startTime = startYear + "-" + startMonth + "-" + startDay + "T" + startHour + ":" + startMin + ":00";
			var endTime = startYear + "-" + startMonth + "-" + startDay + "T" + startHour + ":" + startMin + ":00";

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
			    },
			    "reminderMinutesBeforeStart": 0,
			    "isReminderOn": true,
			};
			return event;
		}
	}
}

function randomElement(array) {
	return array[ Math.floor(Math.random() * array.length) ];
}

function randomIndex(array) {
	return Math.floor(Math.random() * array.length);	
}




















