/*	--- TO DO ---
	[ ] Repeat
	[ ] Database Integration
	[ ] Delete Events
	[ ] Leap Year 
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

// Take in POST request and create calender events for each day
function calendarData(req) {
	var reminders = req.body.txtBx_reminders.split("\n");

	// Get reminders
	var index = randomIndex(reminders);
	if (index > -1) {

	  	var reminder = reminders.splice(index, 1);
	  	var subject = reminder[0];
	  	var body = reminder[0];
	  	usedReminders.push(reminder);

		// Monday Data
		if (req.body.chk_mon == 'on') {
			calendarEvents.push(
				createEvent(dayNum.MON, req.body.num_mon_hour, req.body.num_mon_min, subject, body)
			);
		}

		// Tuesday Data
		if (req.body.chk_tues == 'on') {
			calendarEvents.push(
				createEvent(dayNum.TUES, req.body.num_tues_hour, req.body.num_tues_min, subject, body)
			);
		}
	}
	return calendarEvents;
}
exports.calendarData = calendarData;



function createEvent(dayNum, startHour, startMin, subject, body) {

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
	    }
	};
	return event;
}

function randomElement(array) {
	return array[ Math.floor(Math.random() * array.length) ];
}

function randomIndex(array) {
	return Math.floor(Math.random() * array.length);	
}




















