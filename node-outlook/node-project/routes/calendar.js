var express = require('express');
var router = express.Router();
var authHelper = require('../helpers/auth');
var graph = require('@microsoft/microsoft-graph-client');
var event = require('../helpers/event');

// Create Calendar Event
router.post('/', async function(req, res) {

	// Create Calendar Data from User Form
	var calendarEvents = event.calendarData(req);

	const accessToken = await authHelper.getAccessToken(req.cookies, res);
	const userName = req.cookies.graph_user_name;

	// Access Graph API and Create Calendar Event
	if (accessToken && userName) {

		// Initialize graph client
		const client = graph.Client.init({
			authProvider: (done) => {
				done(null, accessToken);
			}
		});

		try {
			for (var i = 0; i < calendarEvents.length; i++) {
				// Iterate through each calendar event
				var calendarEvent = calendarEvents[i];
				
				const result = await client 
				.api('/me/events')
				.post(calendarEvent);
			}
			res.redirect('/');
		}
		catch (err) {
			console.log(err);
		}
	}
	else {
		res.redirect('/');
	}
});
module.exports = router;