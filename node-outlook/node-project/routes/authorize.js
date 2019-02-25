var express = require('express');
var router = express.Router();
var authHelper = require('../helpers/auth');

// 	--	{home}/authorize/
router.get('/', async function(req, res, next) {

	// Get the auth code
	const code = req.query.code;

	// Use code if it is present
	if (code) {
		let token;

		try {
			token = await authHelper.getTokenFromCode(code, res);
		}
		catch (error) {
			res.render('error', { title: 'Error', message: 'Error exchanging code for token', error: error });
		}

		res.redirect('/');
	}
	else {
		res.render('error', { title: 'Error', message: 'Authorization error', error: { status: 'Missing code parameter' } });
	}
});

router.get('/signout', function(req, res, next) {
	authHelper.clearCookies(res);

	res.redirect('/');
});

module.exports = router;