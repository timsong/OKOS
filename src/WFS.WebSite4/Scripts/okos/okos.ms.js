/// <reference path="/Scripts/jquery-1.5.1-vsdoc.js" />
/// <reference path="/Scripts/messaging.js" />
/// <reference path="jquery-1.6.2-vsdoc.js" />
/// <reference path="jquery.scrollTo.js" />
(function (window) {
	/** setup __base class ***/
	function __base(config) {
		if (this.init) {
			this.init(config);
		}
	}
	jQuery.validator.addMethod("uniqueNameX", function (value, element) {
		var url = $(element).attr('msurl') + '/' + value;
		var r = false;
		ms.ajax.send({ url: url
			, async: false
			, successHandler: function (results) {
				if (parseInt(results.Subject) == 0) {
					r = true;
				}
			}
			, errorHandler: function (r) {
				r = false;
			}
		});
		return r;
	}, "* Name must be unique.");

	$(document).ready(function () {
		$('select').live('keydown', null, function (e) {
			if (e.keyCode == 8) {
				var old = $(this).attr('msold');
				if (typeof old != 'undefined') {
					$(this).val(old);
				}
				return false;
			}
		});
		$('select').live('focus', null, function (e) {
			$(this).attr('msold', $(this).val());
		});
	});

	jQuery.validator.addMethod("password", function (value, element) {
		var val = false;
		return (value.match(/[\d]/g) && value.match(/[\w]/g) && value.match(/[^\d^\g]/) && !value.match(/[\s]+/) && value.length >= 8);
	}, "Password must contain at least 1 number, 1 non alpha numeric character and be at least 8 characters in length.");

	jQuery.validator.addMethod("vPassword", function (value, element) {
		var el = $(element).attr('msid');
		var pwd = $('#' + el).attr('value');
		return pwd == value;
	}, "Passwords must match");

	jQuery.validator.addMethod("validName", function (value, element) {
		var e = /[^\d^\w^\s]+/g;
		var match = value.match(e);
		return !match;
	}, "* Name must be valid.");

	jQuery.validator.addMethod("money", function (value, element) {
		var e = /$[-]*[^\d]+/;
		var match = value.match(e);
		return !match;
	}, "* Please enter valid dollar amount.");

	jQuery.validator.addMethod("zipcode", function (value, element) {
		var valid = false;
		if (value.length == 5) {
			valid = !(value.match(/[^\d]+/));
		}
		else if (value.length == 6 && !value.match(/[^\d]+/)) {
			valid = false;
		}
		else if (value.length == 6) {
			valid = value.match(/[\w][\d][\w][\d][\w][\d]/);
		}

		return valid;
	}, "* Postal Code must be valid.");

	jQuery.validator.addMethod("phone", function (value, element) {
		var valid = false;
		if (value.length == 13) {
			valid = (value.match(/\([\d][\d][\d]\)[\d][\d][\d]-[\d][\d][\d][\d]/));
		}
		return valid;
	}, "* Phone number must be valid.");

	jQuery.validator.addMethod("msdate", function (value, element) {
		var valid = false;
		try {
			var a = (value.match(/^[\d]{1,2}\/[\d]{1,2}\/[\d]{4}$/));
			var b = false;
			if (a) {
				var s = value.split('/');
				b = parseInt(s[0]).between(1, 12) && parseInt(s[1]).between(1, 31) && parseInt(s[2]).between(1999, 2100);
			}
			valid = a && b;
		}
		catch (ex) {
			valid = false;
		}
		return valid;
	}, "* Date must be valid.");
	jQuery.validator.addMethod("date", function (value, element) {
		var valid = false;
		try {
			var a = (value.match(/^[\d]{1,2}\/[\d]{1,2}\/[\d]{4}$/));
			var b = false;
			if (a) {
				var s = value.split('/');
				b = parseInt(s[0]).between(1, 12) && parseInt(s[1]).between(1, 31) && parseInt(s[2]).between(1999, 2100);
			}
			valid = a && b;
		}
		catch (ex) {
			valid = false;
		}
		return valid;
	}, "* Date must be valid.");

	/*** setup queue ***/

	function __queueInfo(config) { if (this.init) { this.init(config); } }
	__queueInfo.prototype = {
		init: function (config) { for (key in config) { this[key] = config[key]; } }
	, key: null
	, id: -1
	, interval: -1
	, lifespan: 0
	, onComplete: function () { }
	, onInterval: function () { }
	, completedHandlers: []
	};

	function __queue(config) { if (this.init) { this.init(config); } }
	__queue.prototype = {
		init: function (config) { for (key in config) { this[key] = config[key]; } }
	, qIs: []
	, qI: null
	, controller: null
	, interval: 3000
	, intervalId: null
	, finished: false
	, startTime: null, lifespan: 0, initialized: false, launched: false
	, initialize: function (qI) { // first pass
		if (!this.initialized) {
			if (qI.interval > 1000) {
				this.interval = qI.interval;
			}
			this.qIs = [];
			this.qI = qI;
			this.lifespan = qI.lifespan;
			this.initialized = true;
		}
		this.startTime = new Date();
		this.qIs.push(qI);
	}
	, submitQueueInfo: function (qI) {
		this.initialize(qI);
		this.launch();
	}
	, complete: function () {
		var s = this;
		clearInterval(this.intervalId);
		if ($.isFunction(this.qI.onComplete)) {
			this.qI.onComplete.call(this, this.qI);
		}
		$(this.qIs).each(function (idx) {
			if (this != s.qI && $.isFunction(this.onComplete)) {
				this.onComplete.call(s, this);
			}
		});
		this.launched = false;
	}
	, processInterval: function () {
		var s = this;
		var dif = new Date() - this.startTime;
		if (dif > this.lifespan) {
			this.complete();
		}
		if ($.isFunction(this.qI.onInterval)) {
			this.qI.onInterval.call(this, this.qI);
		}
		$(this.qIs).each(function (idx) {
			if (this != s.qI && $.isFunction(this.onInterval)) {
				this.onInterval.call(s, this);
			}
		});
	}
	, launch: function () {
		var s = this;

		if (!this.launched) {
			this.startTime = new Date();
			this.intervalId = setInterval(function () {
				s.processInterval.call(s);
			}, this.interval);
			this.launched = true;
		}
	}
	};
	/*** controller ***/
	function __queueController(config) { if (this.init) { this.init(config); } }
	__queueController.prototype = {
		init: function (config) { for (key in config) { this[key] = config[key]; } }
	, queues: []
	, queue: function (qI) {
		if (!this.queues[qI.key]) {
			this.create(qI);
		}
		var q = this.queues[qI.key]; // get the queue
		q.submitQueueInfo(qI); // submit the queue for processing
		q.launch();
	}
	, create: function (qI) {
		var q = new __queue();
		this.queues[qI.key] = q;
	}
	}

	window.ms = {};

	window.__base = __base;
	window.__queueInfo = __queueInfo;
	window.__queue = __queue;
	window.__queueController = __queueController;
	$(document).ready(function () {
		window.onerror = function (e) {
			ms.msg.sendMsg('sysWarning', e);
		};
	});

	ms.decEventBind = {
		init: function () {
			function setup() {
				ms.decEventBind.apply('click');
				ms.decEventBind.apply('clicknav', ms.decEventBind.applyForClickNav);
				ms.decEventBind.apply('mask', ms.decEventBind.applyForMask);
				ms.decEventBind.apply('datepicker', ms.decEventBind.applyForDatePicker);
				ms.decEventBind.apply('mouseleave');
				ms.decEventBind.apply('mouseenter');
				ms.decEventBind.apply('keyup');
				ms.decEventBind.apply('keydown');
				ms.decEventBind.apply('keypress');
				ms.decEventBind.apply('blur');
				ms.decEventBind.apply('focus');
				ms.decEventBind.apply('change');
			};
			ms.event.on({
				key: 'ajaxCall'
				, func: function () {
					setup();
				}
			});
			setup();
		}
		, apply: function (type, cb) {
			$('*[ms' + type + ']').each(function (idx) {
				if ($.isFunction(cb)) cb.call(ms.decEventBind, this);
				else ms.decEventBind.applyFor(this, type);
			});
		}
		, applyForDatePicker: function (selector) {
			var attname = 'mssetdatepicker';
			var att = $(selector).attr(attname);
			if (att) return;
			$(selector).datepicker();
		}
		, applyForMask: function (selector) {
			var attname = 'mssetmask';
			var att = $(selector).attr(attname);
			if (att) return;
			$.mask.definitions['*'] = '[]+';
			$(selector).attr(attname, true);
			var mask = $(selector).attr('msmask');
			var maskcb = $(selector).attr('msmaskhandler');
			var maskconfig = $(selector).attr('msconfig');
			if (typeof $(selector).mask != 'undefined') {
				if (mask == "currency") {
					var config = {};
					if (typeof maskconfig != 'undefined') {
						config = JSON.parse(maskconfig.replace(/\'/g, '"'));
					}
					$(selector).maskMoney(config);
				}
				else if (typeof maskcb != undefined) {
					var f = eval(maskcb);
					$(selector).mask(mask, f);
				}
				else {
					$(selector).mask(mask);
				}
			}
			else {
				throw new Error("Attempt to use msmask failed because jquery.maskedinput.js is not loaded.");
			}
		}
		, applyForClickNav: function (selector) {
			var attname = 'mssetclicknav';
			var att = $(selector).attr(attname);
			if (att) return;
			$(selector).attr(attname, true);
			$(selector).click(function (e) {
				var href = $(selector).attr('msclicknav');
				var targ = $(selector).attr('mstarget');
				if (href && targ && targ == "_blank") {
					window.open(href);
				}
				else if (href) {
					document.location = href;
				}
			});
		}
		, applyFor: function (selector, type) {
			var att = $(selector).attr('msset' + type);
			if (att) return;
			$(selector).attr('msset' + type, true);
			$(selector)[type](function (e) {
				var attr = $(selector).attr('ms' + type);
				var s = this;
				$(attr.split(' ')).each(function (idx) {
					var t = this;
					var func = eval(t.toString());
					if (func) {
						func.call(s, e, type);
					}
					else {
						throw new Error('No func {x} was found to handle'.bind({ x: t }));
					}
				});

				e.stopImmediatePropagation();
			});
		}
	};
	ms.event = {
		events: []
		, on: function (options) {
			var options = $.extend({ func: function () { }, scope: this, key: 'none', fault: true, source: 'anon' }, options);
			ms.msg.sendMsg('sysWarning', 'EventHandler ' + options.key + ' reg by ' + options.source + '.');
			var ea = this.get(options.key);
			ea.push(options);
		}
		, get: function (eventKey) {
			if (!this.events[eventKey]) {
				this.events[eventKey] = [];
			}
			return this.events[eventKey];
		}
		, fire: function (eventKey) {
			var args = Array.prototype.slice.call(arguments, 1);
			if (ms.settings.showSysWarnings()) {
				var outArgs = '';
				$(args).each(function (idx) {
					outArgs += '| ' + args[0];
				});
				ms.msg.sendMsg('sysWarning', 'Event ' + eventKey + ' firing ' + ' with parms: ' + outArgs + '.');
			}
			var arr = this.get(eventKey);
			$(arr).each(function (idx) {
				var s = this;
				ms.err.exec({ rethrow: s.fault, meth: function () {
					s.func.apply(s.scope, args);
				}
				});
				ms.msg.sendMsg('sysWarning', 'EventHandler ' + s.func.toString() + ' firing.');
			});
		}
	};

	ms.querystring = {
		strings: []
		, stringsSet: false
		, get: function (key) {
			if (!this.stringsSet) {
				this.parse();
				this.stringsSet = true;
			}
			if (this.strings[key]) {
				return this.strings[key];
			}
			return '';
		}
		, parse: function () {
			var fullstring = window.location.search.substring(1);
			var splitstring = fullstring.split("&");
			var final = [];
			$(splitstring).each(function (idx) {
				var partial = this.split("=");
				final[partial[0]] = partial.length > 1 ? partial[1] : '';
			});
			this.strings = final;
		}
	};
	ms.settings = {
		sysWarnings: false
		, showSysWarnings: function () {
			var show = false;
			if (arguments.length >= 1) {
				this.sysWarnings = arguments[0];
			}
			show = this.sysWarnings || ms.querystring.get('sWarnings');
			return show;
		}
	};

	ms.ajax = {
		init: function () {

		}
		, send: function (options) {
			ms.utility.ajax(options);
		}
		, handleResult: function (result) {
			if (result.Status == 0 && result.Messages.length > 0) {
				$(result.Messages).each(function (idx) {
					ms.msg[this.Type == 0 ? 'sendError' : 'sendInfo'](this.DisplayMessage);
				});
			}
			else if (result.Messages.length > 0) {
				$(result.Messages).each(function (idx) {
					ms.msg[this.Type == 0 ? 'sendError' : 'sendInfo'](this.DisplayMessage);
				});
			}
		}
	};
	ms.err = {
		exec: function (options) {
			options = $.extend({
				rethrow: true
				, meth: function () { }
				, scope: this
				, onerror: function () { }
			}, options);
			var ex;
			if (options.meth) {
				try {
					options.meth.call(options.scope);
				}
				catch (exx) {
					options.onerror.call(options.scope, exx);
					ex = exx;
				}
			}
			if (options.rethrow && ex) {
				throw ex;
			}
		}
	};

	var __utility = $.extend(true, function () { }, __base);
	__utility.prototype = {
		ajax: function (options) {
			options = $.extend({ type: 'GET', async: true, cache: false
			, success: function (data) {
				if ($.isFunction(options.successHandler)) options.successHandler.call(scope, data);
				else ms.ajax.handleResult(data);
				ms.event.fire('ajaxCall');
			}
			, error: function (data) {
				if ($.isFunction(options.errorHandler)) {
					options.errorHandler.call(scope, data);
				}
				ms.msg.sendMsg('sysWarning', data.responseText);
			}
			}, options);
			//options.url = this.prepUrl(options.url);
			ms.msg.sendMsg('sysWarning', 'ms.ajax.send: url[' + options.url + '] type["' + options.type + '"] data["' + options.data + '"]');
			var scope = this;
			$.ajax(options);
		}
	, token: null
	, debug: false
	, setDebug: function (value) {
		this.debug = value;
	}
	, getAttr: function (el, key, defaultValue) {
		return $(el).attr(key) ? $(el).attr(key) : defaultValue;
	}
	, setSec: function (value) {
		this.token = value;
	}
	, getSec: function () {
		return this.token;
	}
	, prepUrl: function (url) {
		var del = '?';
		if (url.indexOf('?') > 0) {
			del = '&';
		}
		url = url + del + this.getRnd();
		return url;
	}
	, getRnd: function () {
		return Math.round(1000000 * Math.random());
	}
	, datasets: []
	, loadForm: function (data, options) {
		var options = $.extend({
			containerId: ''
			, data: null
			, type: 'GET'
			, url: ''
			, preload: function (data) { return true; }
			, success: function () { }
			, traditional: false
		}, options);

		var sel = String.format('#{0}', options.containerId);
		var selSave = String.format('#{0}', options.saveId);
		var selCancel = String.format('#{0}', options.cancelId);
		$('.widgetGForm').html('');
		if ($(sel + 'Form').length == 0) {
			$(document.body).append(String.format('<form id="{0}Form"><div id="{0}" class="widgetGForm"></div></form>', options.containerId));
		}
		if (options.preload(data)) {
			var ajaxoptions = {
				url: options.url
				, type: options.type
				, cache: false
				, success: function (data) {
					$(sel).html(data).css('position', 'absolute').css('top', '30%').css('left', '30%').draggable();
					options.success.call();
				}
				, traditional: options.traditional
			, error: function (data) {
				alert(data.responseText);
			}
			};
			if (options.data) {
				ajaxoptions.data = options.data;
			}

			ms.utility.ajax(ajaxoptions);
		}

	}
	, preloadImgs: function () {

	}
	, initPage: function () {
		this.preloadImgs();
		this.qCtrl = new __queueController({});
	}
	, getDataset: function (options) {
		var scope = this;
		if (!this.datasets[options.url]) {
			scope.ajax({
				url: scope.prepUrl(options.url)
				, success: function (data) {
					scope.datasets[options.url] = data;
					options.complete.call(scope, data);
				}
				, async: false
			});
		}
		else {
			options.complete.call(scope, scope.datasets[options.url]);
		}
	}
	, parseDate: function (rawDate) {
		return new Date(parseInt(rawDate.substr(6)));
	}
	, getMonthAbbr: function () {
		var monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];

		return function (month) {
			if (typeof month === 'object' && typeof month.getMonth === 'function') {
				return monthNames[month.getMonth()];
			} else if (typeof month === 'number') {
				return monthNames[month];
			}
		}
	} ()
	};
	ms.store = {
		get: function (key, defaultValue) {
			if (!window.localStorage || this.shouldUseCookie()) {
				var cookies = document.cookie.split(';');
				var value;
				$(cookies).each(function (idx) {
					var kvp = this.split('=');
					if (kvp[0].replace(/^\s+|\s+$/g, "") == key)
						value = kvp[1];
				});
				return value ? JSON.parse(unescape(value)) : defaultValue;
			}
			else {
				return window.localStorage[key] ? JSON.parse(window.localStorage[key]) : defaultValue;
			}
		}
		, forceCookie: function () {
			this.useCookie = true;
		}
		, shouldUseCookie: function () {
			var u = this.useCookie;
			this.useCookie = false;
			return u;
		}
		, useCookie: false
		, set: function (key, value) {
			if (!window.localStorage || this.shouldUseCookie()) {
				var date = new Date();
				date.setDate(date.getDate() + 1000);
				var val = escape(JSON.stringify(value)) + ';expires=' + date.toString();
				document.cookie = key + '=' + val;
			}
			else {
				window.localStorage[key] = JSON.stringify(value);
			}
		}
	};
	ms.tile = {
		init: function () {
			this.addBeh();
		}
		, hoverTile: function (el) {
			$(el).addClass('tileHover');
		}
		, hoverTileOut: function (el) {
			$(el).removeClass('tileHover');
		}
		, addBeh: function () {
			$('.tile').live('mouseenter', null, function () {
				ms.tile.hoverTile(this);
			});
			$('.tile').live('mouseleave', null, function () {
				ms.tile.hoverTileOut(this);
			});
		}
	};

	ms.msg = {
		msgOptions: { el: '#messageBox', contentEl: '#messageBox', errorClass: 'errorMessageBox', infoClass: 'infoMessageBox', waitingClass: 'waitingMessageBox', warningClass: 'warningMessageBox' }
	, qCtrl: new __queueController({})
	, clearSys: function () {
		$('#sysWarnings').html('');
	}
	, sendMsg: function (type, msg) {
		///	<summary>
		///		 ms.msg.sendMsg(options) - sends a message to the user.  If the 'sysWarning' type is specified the msg will be displayed on the page only if the sysWarning param is set.
		///	</summary>
		///	<param name="type" type="String">
		///		A string value that indicates which type of message should be sent.  ( 'error' | 'info' | 'waiting' | 'warning' | 'sysWarning' );
		///	</param>
		///	<param name="msg" type="String">
		///		A string value that will be displayed to the user.
		///	</param>
		if (type == 'sysWarning') {
			if (ms.settings.showSysWarnings()) {
				if ($('#sysWarnings').length == 0) {
					$('body').append('<h5>System Warnings: <a href="javascript:ms.msg.clearSys()">clear</a></h5><div id="sysWarnings" style="width: 100%, max-height: 800px; overflow: auto; border: dotted red 1px;"><div>');
				}
				$('#sysWarnings').prepend('<div style="background: lightyellow; border-bottom: solid 1px red; color: red: width: 100%; max-height: 100px; overflow: auto;">' + msg + '</div>');
			}
			return;
		}
		$(this.msgOptions.el).show();
		for (key in this.msgOptions) {
			try {
				if (key.indexOf('Class')) {
					$(this.msgOptions.el).removeClass(this.msgOptions[key]);
				}
			}
			catch (ex) {

			}
		}
		$(this.msgHandlers).each(function (idx) { // alert subscribers
			this.call(this, type, msg);
		});
		$(this.msgOptions.el).addClass(this.msgOptions[type + 'Class']);
		if (this.msgVm) {
			this.msgVM.msg(msg);
		}
		else {
			Msg.alert('', type, msg, msg);
		}
		$(this.msgOptions.contentEl).html(msg);
		this.clearMessage();
	}
	, msgHandlers: []
	, subscribe: function (cb) {
		///	<summary>
		///		 ms.msg.subscribe(options) - subscribe to message evenms.
		///	</summary>
		///	<param name="cb" type="Callback">
		///		A js function that will be called when a message occurs.  The function will be called with type and msg parameters.
		///	</param>
		this.msgHandlers.push(cb);
	}
	, msgVm: null
	, init: function (options) {
		if ($('#messageBox').length == 0) {
			$('body').append('<div id="messageBox" data-bind="text: msg"></div>');
		}
		$('#messageBox').hide();
		options = $.extend({ selector: '#messageBox', preBind: function () { } }, options);
	}

	, setMsgOptions: function (options) {
		this.msgOptions = $.extend(this.msgOptions, options);
	}
	, sendError: function (msg) {
		///	<summary>
		///		 ms.msg.sendError(msg) - sends an error message.
		///	</summary>
		///	<param name="msg" type="String">
		///		The message to display.
		///	</param>
		this.sendMsg('error', msg);
	}
	, sendInfo: function (msg) {
		///	<summary>
		///		 ms.msg.sendInfo(msg) - Sends an info level message to the user.
		///	</summary>
		///	<param name="msg" type="String">
		///		A string value that will be displayed to the user.
		///	</param>
		this.sendMsg('info', msg);
	}
	, sendWarning: function (msg) {
		///	<summary>
		///		 ms.msg.sendWarning(msg) - Sends a warning level message to the user. *** NOT A SYSWARNING
		///	</summary>
		///	<param name="msg" type="String">
		///		A string value that will be displayed to the user.
		///	</param>
		this.sendMsg('warning', msg);
	}
	, sendWaiting: function (msg) {
		///	<summary>
		///		 ms.msg.sendWaiting(msg) - Sends a waiting level message to the user. 
		///	</summary>
		///	<param name="msg" type="String">
		///		A string value that will be displayed to the user.
		///	</param>
		this.sendMsg('waiting', msg);
	}
	, clearMessage: function () {
		var qI = new __queueInfo({
			key: "PAGEMESSAGE"
			, interval: 500
			, lifespan: 3000
			, onComplete: function () {
				Msg.hide();
			}
		});
		this.qCtrl.queue(qI);
	}
	, sendMessage: function (msg) {
		///	<summary>
		///		 ms.msg.sendMessage(msg) - Sends an info level message to the user. 
		///	</summary>
		///	<param name="msg" type="String">
		///		A string value that will be displayed to the user.
		///	</param>
		this.sendInfo(msg);
	}
	};
	$(document).ready(function () {
		ms.tile.init();
		ms.msg.init({});
		ms.ajax.init();
		ms.decEventBind.init();
	});
	ms.dyn = {
		parse: function (subject, path) {
			if ($.isArray(path)) {
				while (path.length > 0) {
					subject = subject[path.pop()];
				}
			}
			else {
				subject = ms.dyn.parse(subject, path.split('.').reverse());
			}
			return subject;
		}
	};

	ms.debug = false;

	ms.utility = new __utility();
	ms.utility.initPage();

	ms.ml = {
		append: function (el, html) {
			$(el).append(html);
			ms.decEventBind.init();
		}
	, prepend: function (el, html) {
		$(el).prepend(html);
		ms.decEventBind.init();
	}
	, toggleClass: function (e) {
		var cls = $(this).attr('msvalues').toString().split(',');
		var s = this;
		var found = false;
		var stop = false;
		$(cls).each(function (idx) {
			if (stop) return;
			if (found) {
				stop = true;
				$(s).addClass(this.toString());
			}
			else if ($(s).hasClass(this)) {
				found = true;
				$(s).removeClass(this.toString());
			}
		});
		if (!stop) {
			$(s).addClass(cls[0].toString());
		}
	}
	, html: function (el, html) {
		$(el).html(html);
		ms.decEventBind.init();
	}
	};

	ms.modal = {
		confirmF: function () {
			var value = $(this).attr('data-value');
			ms.modal.currentHandler.call(this, value);
			$('#modalConfirm').trigger('reveal:close');
		}
	, currentHandler: function () { }
	, confirm: function (title, handler) {
		this.currentHandler = handler;
		ms.ml.html('#modalConfirmTitle', title);
		$('#modalConfirm').reveal({
			animation: 'fadeAndPop'
			, animationspeed: 300
			, closeOnBackgroundClick: true
			, dismissModalClass: 'close-reveal-modal'
		});
	}
	, confirmValues: { YES: 'YES', NO: 'NO', CANCEL: 'CANCEL' }
	}

	ms.message = {
		get: function (context, messageSel, msgs) {
			ms.ml.html(messageSel, '');
			msgs = $.extend(msgs, {
				SYSTEMERROR: 'A serious error has occurred.  If this continues please contact an administrator.'
				, GENERALSAVE: 'Save was successful.'
			});
			var msgC = {
				sendInfo: function (msg) {
					$(messageSel).removeClass('secondary alert success');
					msgC.send(2, msg);
				}
			, sendError: function (msg) {
				msgC.send(4, msg);
			}
			, sendWarning: function (msg) {
				msgC.send(8, msg);
			}
			, msgs: msgs
			, qCtrl: new __queueController({})
			, getMsg: function (status, msg) {
				var scope = this;
				var s = msgC.lookupStatus(status);
				var pid = '{c}_{id}'.bind({ c: context.replace(/[^\w^\d]+/g, '_'), id: msg.replace(/[^\w^\d]+/g, '_') }).toLowerCase();
				var id = '__msg_{id}'.bind({ id: pid });
				var msgMl = '<div id="{id}" class="alert-box {cls} round">{msg}</div>'.bind({ id: id, cls: s.cls, stat: s.title, msg: msg });
				return {
					send: function () {
						ms.ml.append(messageSel, msgMl);
						var qI = new __queueInfo({
							key: '{c}-PAGEMESSAGE'.bind({ c: context })
							, interval: 500
							, lifespan: 2000
							, onComplete: function () {
								if (s.onFClear) {
									$('#' + id).fadeOut('fast', function () {
										$('#' + id).remove();
									});
								}
							}
						});
						scope.qCtrl.queue(qI);
					}
					, status: s
					, msg: msgMl
				};
				return;
			}
			, send: function (status, msg) {
				if (msg.Messages === undefined) {
					var m = msgC.getMsg(status, msg);
					m.send();
				}
				else {
					msg.Messages.select(function () {
						var msg = this;
						var m = msgC.getMsg(status, this.Text);
						m.send();
					});
				}
			}
			, lookupStatus: function (id) {
				return msgC.statususes[id];
			}
			, statususes: {
				1: { title: 'System Error', cls: 'alert', onFClear: false }
				, 2: { title: '', cls: 'success', onFClear: true }
				, 4: { title: 'Error', cls: 'alert', onFClear: false }
				, 8: { title: 'Warning', cls: '', onFClear: true }
			}
			}
			return msgC;
		}
		, showSystemError: function () {
			var msg = $(this).find('.bigmessage').html();
			alert(msg);
		}
	}
})(window);