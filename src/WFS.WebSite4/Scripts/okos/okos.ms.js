/// <reference path="/Scripts/jquery-1.5.1-vsdoc.js" />
/// <reference path="/Scripts/messaging.js" />

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
	ms.pos = {
		setPos: function (e, sel) {
			var config = {};
			config.element = e.srcElement;
			config.x = e.clientX;
			config.y = e.clientY;
			var width = $(sel).width();
			var height = $(sel).height();
			var offsetY = 10, offsetX = 20;
			var x = (config.x + width + offsetX > $(window).width()) ? config.x - (width + offsetX) : config.x + offsetX;
			var y = (config.y + height + offsetY > $(window).height()) ? config.y - (height + offsetY) : config.y + offsetY;
			$(sel).css('position', 'fixed').css('top', y).css('left', x);
		}
	};
	ms.fmt = {
		phone: function (rawPhone) {
			var phone = rawPhone;
			if (rawPhone.substring(0, 1) == 1) phone = phone.substring(1);
			if (rawPhone.length == 10) {
				phone = '(' + rawPhone.substr(0, 3) + ')' + rawPhone.substr(3, 3) + '-' + rawPhone.substr(6, 4);
			}
			return phone;
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
					ms.log.log({
						title: 'Error occurred'
					, body: exx.message
					, type: 'Exception'
					});
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

				ms.log.log({
					title: 'Error occurred during ajax call ms.ajax.send: url[' + options.url + '] type["' + options.type + '"] data["' + options.data + '"]'
					, body: escape(data.responseText)
					, type: 'Exception'
				});
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
	ms.form = {
		dirtify: function (options) {
			options = $.extend({ selector: '', dirtyClass: '', showDirty: false }, options);
			$(options.selector).each(function (idx) {
				$(this).attr('ts-original', $(this).val());
			});
		}
	};
	ms.search = {
		within: function (subject, find) {
			// prepare search text
			var finalFound = false;
			var notFound = false;
			find = find.toLowerCase().replace(/\s\s/g, ' ').replace(/[\s]*\+[\s]*/g, '+').replace(/\s+/g, '|');
			var finds = find.split('+');
			$(finds).each(function (idx) {
				var findOrs = this.split('|');
				var found = false;
				$(findOrs).each(function (idxsub) {
					if (subject.indexOf(this) >= 0 && this.length > 0)
						found = true;
				});
				if (!notFound) {
					finalFound = (subject.indexOf(this) >= 0) || found;
					if (!finalFound) notFound = true;
				}
			});
			ms.msg.sendMsg('sysWarning', finds.length + ' -- ' + subject);
			return finalFound;
		}
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
	ms.win = {
		templates: []
		, init: function () {
			$('.emailLink').click(function (e) {
				if ($(this).attr('href') != '#') {
					e.stopImmediatePropagation();
				}
			});
			ms.ajax.send({ url: '/System/ModalWindow'
				, successHandler: function (data) {
					ms.win.templates['modal'] = data;
				}
			});
		}
		, closeModal: function (id) {
			///	<summary>
			///		 ms.win.closeModal(id) - Closes a modal window by removing it from the DOM.
			///	</summary>
			///	<param name="id" type="String">
			///		A string id that matches the call to ms.win.modal.  If no id was passed into the original call a random id was returned.
			///	</param>
			$('#modalWindowOuter_' + id).remove();
		}
		, resizeModal: function () {
			$('.modalWindowInner').css('left', ($(window).width() - $('.modalWindowInner').width()) / 2);
		}
		, prepActionMenu: function (id) {
			var options = this.windows[id];
			var winid = '#' + options.windowIdSuffix + id;
			if ($(winid).find('.addToFormAction').length > 0) {
				var menuActionHtml = $(winid).find('.addToFormAction').html().replace(/msset/g, 'xyz');
				ms.ml.html($('#modalWindowOuter_' + id).find('.' + options.headerSuffix), menuActionHtml);
				$(winid).find('.addToFormAction').remove();
			}
		}
		, windows: []
		, modal: function (options) {
			///	<summary>
			///		 ms.win.model(options) - This function accepts a js params object.
			///	</summary>
			///	<param name="options" type="jQuery">
			///		expected options properties:
			///			 [ id: an id that is bound into the dom element (default is rnd). ] -&b
			///			 [ url: the url of the view to load. ] -
			///			 [ selector: non-functional now. ] -
			///			 [ binder: a callback used once the view is succesfully loaded.  Use to bind events, post load. ]
			///	</param>
			///	<returns type="Integer" />
			var options = $.extend({
				windowInnerClass: 'modalWindowInner'
				, closeLinkSuffix: 'modalCloseLink_'
				, headerSuffix: 'headerMenuAction'
				, windowShellIdSuffix: 'modalWindowOuter_'
				, windowIdSuffix: 'modalWindow_'
				, headerIdSuffix: 'modalWindow_header_'
				, id: ms.utility.getRnd(), clickAway: false, url: null, selector: null, title: 'Window', htmlResult: null, binder: function () { }
			}, options);
			this.windows[options.id] = options;
			var outerId = '#' + options.windowShellIdSuffix + options.id;
			if (options.url && $(outerId).length == 0) {
				var htmlx = $.format(ms.win.templates['modal'], options.id);
				$('body').append(htmlx);
				var winid = '#' + options.windowIdSuffix + options.id;
				$('#' + options.closeLinkSuffix + options.id).click(function (e) {
					ms.win.closeModal(options.id);
				});
				ms.ajax.send({
					url: options.url
					, asynch: false
					, successHandler: function (data) {
						if (typeof data.HtmlResult != 'undefined') {
							ms.ml.html(winid, data.HtmlResult);
						}
						else {
							ms.ml.html(winid, data);
						}
						if (options.clickAway) {
							$('.' + options.modalWindowInner).click(function (e) {
								e.stopImmediatePropagation();
							});
							$(winid).click(function (e) {
								e.stopImmediatePropagation();
							});
							$(outerId).click(function (e) {
								e.stopImmediatePropagation();
								ms.win.closeModal(options.id);
							});
						}
						ms.win.resizeModal();
						ms.win.prepActionMenu(options.id);
						$(window).resize(function () {
							ms.win.resizeModal();
						});
						$('#' + options.headerIdSuffix + options.id).html(options.title);
						options.binder.call(this);
					}
				});
			}
			else if ($(outerId).length > 0) {
				$(outerId).show();
			}

			if (options.htmlResult) {

				var htmlx = $.format(ms.win.templates['modal'], options.id);
				$('#main').append(htmlx);
				var winid = '#' + options.windowIdSuffix + options.id;
				$('#' + options.closeLinkSuffix + options.id).click(function (e) {
					ms.win.closeModal(options.id);
				});
				setTimeout(function () {
					$(winid).html(options.htmlResult);

					if (options.clickAway) {
						$('.' + options.modalWindowInner).click(function (e) {
							e.stopImmediatePropagation();
						});
						$(winid).click(function (e) {
							e.stopImmediatePropagation();
						});
						$(outerId).click(function (e) {
							e.stopImmediatePropagation();
							ms.win.closeModal(options.id);
						});
					}

					ms.win.resizeModal();
					ms.win.prepActionMenu(options.id);

					$(window).resize(function () {
						ms.win.resizeModal();
					});
					$('#' + options.headerIdSuffix + options.id).html(options.title);
					options.binder.call(this);
				}, 100);
			}

			return options.id;
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
		ms.link.init();
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
	ms.log = {
		logger: { url: '/System/Log' }
		, setLogger: function (options) {
			this.logger = $.extend(this.logger, options);
		}
	   , log: function (options) {

	   }
	};
	ms.convert = {
		toDate: function (raw) {
			var rex = /([\d]+)-([\d]+)-([\d]+)T([\d]+):([\d]+):([\d]+)\.([\d]+)-([\d]+):([\d]+)/;
			var out = rex.exec(raw);
			var date = new Date(parseInt(out[1]), out[2] - 1, parseInt(out[3]), parseInt(out[4]), parseInt(out[5]), parseInt(out[6]), parseInt(out[7]));
			return date;
		}
	};

	ms.enu = {
		enums: []
	, get: function (eName, value) {
		return this.enums[eName][value];
	}
	, build: function (enumsOut) {
		var s = this;
		$(enumsOut.enumsOut).each(function (idx) {
			var n = this.name;
			s.enums[n] = [];
			$(this.values).each(function (idx) {
				s.enums[n][idx] = { value: this.value, text: this.text };
			});
		});
	}
	};
	ms.viewModel = {
		adapters: []
			, get: function (adapterName) {
				return new this.adapters[adapterName]();
			}
			, bind: function (adapterName, object) {
				if (!this.adapters[adapterName]) {
					throw new Error('Adapter [' + adapterName + '] not found.');
				}
				var a = this.get(adapterName);
				if (a.defer) {
					$(document).ready(function () {
						a.bind(object);
					});
				}
				else {
					a.bind(object);
				}
			}
			, register: function (adapterName, adapter) {
				if (!adapter.prototype.bind) {
					throw new Error(adapterName + ' does not contain method "bind".');
				}
				this.adapters[adapterName] = adapter;
			}
	};
	ms.menu = {
		enableDisabledItems: function (selector) {
			if (typeof (selector[0]) === "undefined") selector = '';
			$('.disabledMenuItem a' + selector).unbind('click');
			$('.disabledMenuItem a' + selector).removeClass('disabledMenuItem');
		},
		setDisabledItems: function () {
			$('.disabledMenuItem a').click(function (e) {
				e.preventDefault();
			});
		}
	};

	ms.checkbox = {
		toggleKey: function (e) {
			if (32 == e.keyCode) ms.checkbox.toggle.call(this, e);
		}
		, toggle: function (e) {
			var id = $(this).attr('mscbid');
			var trueImg = ms.utility.getAttr(this, 'mstrue', '/Content/images/true.jpg');
			var falseImg = ms.utility.getAttr(this, 'msfalse', '/Content/images/false.jpg');
			var val = parseBool($('input[type="hidden"][mscbid="' + id + '"]').val());
			$(this).attr('value', !val);
			$('input[type="hidden"][mscbid="' + id + '"]').val(!val);
			$('img[mscbid="' + id + '"]').attr('src', val ? falseImg : trueImg);
		}
	};

	ms.utility = new __utility();
	ms.utility.initPage();
	$(document).ready(function () {
		ms.win.init();
		ms.menu.setDisabledItems();
	});
	/*** jquery extensions ***/
})(window);

/// <reference path="jquery-1.6.2-vsdoc.js" />

(function (window) {
	ani = {
		interval: 500
		, defers: []
		, start: function (id) {
			var a = ani.defers[id];
			if (a && a.live == false) {
				a.live = true;
				a.s.call(this, function () {
					a.running = false;
				});
			}
			else if (a && a.live) {
				return;
			}
			else {
				throw new Error("Deferred animation sequence " + id + " not found.");
			}

		}
		, asynchreach: function (next, selector, meth, speed) {
			var s = this;
			$(selector)[meth](speed);
			if ($.isFunction(next)) {
				next.call(s);
			}
		}
		, prep: function (v) {
			return v.replace(/\'/g, '');
		}
		, reachJ: function (next, selector, meth, speed) {
			var s = this;
			$(selector)[meth](speed, function () {
				if ($.isFunction(next)) {
					next.call(s);
				}
			});
		}
		, reachStart: function (next, id) {
			var s = this;
			ani.rDebug('*** STARTING NEW AT ' + id);
			ani.start(id);
		}
		, call: function (next, meth) {
			var s = this;
			var func = eval(meth);
			if ($.isFunction(func)){
				func.call(s);
			}
			if ($.isFunction(next)) {
				next.call(s);
			}
		}
		, addClass: function (next, cls) {
			var s = this;
			$(this).addClass(cls);
			if ($.isFunction(next)) {
				next.call(s);
			}
		}
		, removeClass: function (next, cls) {
			var s = this;
			$(this).removeClass(cls);
			if ($.isFunction(next)) {
				next.call(s);
			}
		}
		, initDefer: function(){
			var sx = this;
			var s = ani;
			s.defers[$(this).attr('id')] = { s: function () {
					ani.process.call(sx);
				}, live: false
			};
		}
		, init: function () {
			var s = this;
			this.registerJ('fadeIn,fadeOut,show,hide,slideDown,slideUp');
			this.registerAsynchJ('fadeIn,fadeOut,show,hide,slideDown,slideUp');
			$('*[ani="defer"]').each(function (idx) {
				ani.initDefer.call(this);
			});
			$('*[ani="start"]').each(function (idx) {
				var attr = $(this).attr('ani');
				ani.process.call(this);
			});
		}
		, registerAsynchJ: function (jMethName) {
			var jarray = jMethName.split(',');
			var s = this;
			$(jarray).each(function (idx) {
				var a = 'asynch' + this;
				var b = this;
				s[a] = function (next, speed) {
					var speed = !isNaN(speed) ? 'fast' : speed;
					var s = this;
					$(s)[b](speed);
					if ($.isFunction(next)) {
						next.call(s);
					}
				};
			});
		}
		, registerJ: function (jMethName) {
			var jarray = jMethName.split(',');
			var s = this;
			$(jarray).each(function (idx) {
				var a = this;
				s[this] = function (next, speed) {
					var p = [];
					if (!isNaN(speed)&&speed!=''){
						speed = parseInt(speed);
					}
					p.push(speed);
					p.push(function () {
						if ($.isFunction(next)) {
							next.call(s);
						}
					});
					var s = this;

					$(s)[a](p[0], p[1]);
				};
			});
		}
		, isDebug: false
		, debug: function (next) {
			var s = this;
			ani.isDebug = true;
			if ($.isFunction(next)) {
				next.call(s);
			}
		}
		, getParms: function () {
			var arr = $(this).attr('parms');
			return arr;
		}
		, wait: function (next, dur) {
			var s = this;
			if ($.isFunction(next)) {
				setTimeout(function () {
					next.call(s);
				}, dur);
			}
		}
		, debugSay: function (msg, style) {
			if (ani.isDebug) $('#aniDebugBox').append('<div style="' + style + '">' + msg + '</div>');
		}
		, sequence: function (next, reverse) {
			reverse = reverse=='true';
			ani.debugSay('-------> sequence ' + reverse);
			function _act() {
				var f = seq.pop();
				if (f) {
					ani.process.call(f, function () {
						_act.call();
					});
				}
				else if ($.isFunction(next)) {
					next.call(this);
				}
			};
			var s = this;
			var seq = [];
			var kids = $(s).children('*[ani]');
			$(kids).each(function (idx) {
				seq.push(this);
			});
			seq = reverse==true ? seq : seq.reverse();
			ani.debugSay('-------> count: ' + seq.length);
			_act();
		}
		, getTime: function () {
			var currentTime = new Date()
			var hours = currentTime.getHours()
			var minutes = currentTime.getMinutes()
			var seconds = currentTime.getSeconds();
			return hours + ':' + minutes + ':' + seconds;
		}
		, rDebugClear: function () {
			$('#aniDebugBox').html('');
		}
		, stops: []
		, stop: function(id){
			ani.stops[id] = true;
			$('#' + id).attr('ani', 'defer');
			var el = $('#' + id)[0];
			ani.initDefer.call(el);
		}
		, restart: function(id){
			ani.stops[id] = false;
			ani.defers[id].live = false;
			ani.start(id);
		}
		, rDebug: function (action, params) {
			if (!ani.isDebug) return;
			if ($('#aniDebugBox').length == 0) {
				$('body').append('<div id="aniDebugBoxOuter" style="border: solid 1px red; min-width: 300px; font-size: 7pt; position: fixed; top: 1px; left: 1px; max-height: 500px; overflow: auto;"></div>');
				$('#aniDebugBoxOuter').append('<div><a id="aniDebugBoxClear" href="javascript:ani.rDebugClear()">clear</a></div>');
				$('#aniDebugBoxOuter').append('<div id="aniDebugBox"></div>');
				for (key in ani.defers) {
					ani.rDebug('DEFER: ' + key);
				};
			}
			var parms = ($.isArray(params)) ? '(' + params.join(',') + ')' : '';
			var id = $(this).attr('id') ? $(this).attr('id') : 'no id';
			ani.debugSay(ani.getTime() + '---&gt; ' + id + ' ' + action + parms);
		}
		, rDebugFail: function (action, ex) {
			var id = $(this).attr('id') ? $(this).attr('id') : 'no id';
			ani.debugSay(ani.getTime() + ' EXCEPTION---&gt; ' + $(this).attr('id') + ' ' + action + ' at ' + id + ': ' + ex.message, 'color: red;');
		}
		, act: function (raw, action, next) {
			try {
				var s = this;
				var id = $(this).parents('*[ani="start"]').attr('id');
				if (!id) { 
					id = $(this).attr('id');
				}
				if (ani.stops[id]) return;
				ani.rDebug.call(this, action);
				var params = raw.replace(action, '').replace(/[\s]*\(/g, '').replace(/[\s]*\)/g, '').split(',');
				$(params).each(function (idx) {
					params[idx] = this.replace(/^[\s]*\'/g, '').replace(/\'$/g, '');
				});
				ani.rDebug.call(this, action, params);
				params.splice(0, 0, next);
				var act = ani[action];
				if (!act) { throw new Error('action ' + action + ' is invalid'); }
				act.apply(s, params, function () {
					if ($.isFunction(next)) {
						next.call(this, raw);
					}
				});
			} catch (ex) {
				ani.rDebugFail.call(this, action, ex);
			}
		}
		, process: function (next, shutdown) {
			function _act() {
				var first = actions.pop();
				var raw = first;
				if (first) {
					first = first.replace(/\([^\)]*\)/g, '').replace(/[\t\s]+/g, '');
					ani.act.call(s, raw, first, _act);
				}
				else if ($.isFunction(next)) {
					next.call(this);
				}
				else if ($.isFunction(shutdown)) {
					shutdown.call(this);
				}
			}
			var s = this;
			var action = $(this).attr('action');
			if (!action) return;
			var actions = action.split(';').reverse();
			_act();
		}
	};
	window.ani = ani;

function jGallary(config) {
	for (key in config) {
		this[key] = config[key];
	}
	this.tabs = new Array();
	var scopeMe = this;
	if (this.nextEl)
		$(this.nextEl).bind("click", function() { scopeMe.next(); });

	if (this.previousEl)
		$(this.previousEl).bind("click", function() { scopeMe.previous(); });
		
	if (this.init!=null)
		this.init();

	if (this.animate && !isNaN(this.animateInterval)) {
		setInterval("jQuery['_gal_" + this.name + "'].next()", this.animateInterval);
	}
};

jGallary.prototype = {
	name: null
	, animate: false
	, animateInterval: 1000
	, containerEl: null
	, _currentId: 0
	, speed: 200
	, nextCallBack: null
	, previousCallBack: null
	, gotoCallBack: null
	, nextEl: null
	, previousEl: null
	, effect: "easeInOutSine"
	, setName: function(name) { this.name = name; }
	, getName: function() { return this.name; }
	, addTab: function(tabId) { this.tabs[this.tabs.length] = "#" + tabId; }
	, next: function() {
		if (this._currentId >= this.tabs.length - 1) {
			this._currentId = 0;
			isReset = true;
		}
		else this._currentId++;
		$(this.containerEl).stop().scrollTo($(this.tabs[this._currentId]), this.speed, { easing: this.effect });
		if (this.nextCallBack)
			this.nextCallBack.call(this, this._currentId);
	}
	, previous: function() {

		if (this._currentId <= 0) this._currentId = this.tabs.length - 1;
		else this._currentId--;

		$(this.containerEl).stop().scrollTo($(this.tabs[this._currentId]), this.speed, { easing: this.effect });
		if (this.previousCallBack)
			this.previousCallBack.call(this, this._currentId);

	}
	, gotoTab: function(tabId){
	
		for(key in this.tabs)
		{
			if (this.tabs[key]==tabId)
			{
				$(this.containerEl).stop().scrollTo($(this.tabs[key]), this.speed, { easing: this.effect });
				this._currentId = key;

				if (this.gotoCallBack)
					this.gotoCallBack.call(this, this._currentId);
				return;
			}
		}
		
	}
	, init: function() {

	}
};

ms.loader = {
	clear: function(e){
		e.stopImmediatePropagation();
		var sel = $(this).attr('msselector');
		$(sel).html(results);
	}
	, ajax: function(e, type, data){
		e.stopImmediatePropagation();
		var s = this;
		var url = $(this).attr('msurl');
		var sel = $(this).attr('msselector');
		var callback = $(this).attr('mshandle');
		var opt = { url: url
			, type: type
			, successHandler: function(results){
				if (callback){
					callback = eval(callback);
				}
				if ($.isFunction(callback)){
					callback.call(s, results);
				}
			}
			, errorHandler: function(r){

			}
		};
		if (data){
			opt.data = data;
		}
		ms.ajax.send(opt);
	}
	, get: function(e){
		ms.loader.ajax.call(this, e, 'GET');
	}
	, post: function(e){
		var form = $(this).attr('msform');
		var formData = $(form).serialize();
		ms.loader.ajax.call(this, e, 'POST', formData);
	}
	, load: function(e){
		e.stopImmediatePropagation();
		var url = $(this).attr('msurl');
		var sel = $(this).attr('msselector');
		ms.ajax.send({ url: url
			, successHandler: function(results){
				$(sel).html(results);
			}
		});
	}
}

ms.ml = {
	append: function(el, html) {
		$(el).append(html);
		ms.decEventBind.init();
	}
	, prepend: function(el, html) {
		$(el).prepend(html);
		ms.decEventBind.init();
	}
	, toggleClass: function(e){
		var cls = $(this).attr('msvalues').toString().split(',');
		var s = this;
		var found = false;
		var stop = false;
		$(cls).each(function(idx){
			if (stop) return;
			if (found){
				stop = true;
				$(s).addClass(this.toString());
			}
			else if ($(s).hasClass(this)){
				found = true;
				$(s).removeClass(this.toString());
			}
		});
		if (!stop){
			$(s).addClass(cls[0].toString());
		}
	}
	, html: function(el, html){
		$(el).html(html);
		ms.decEventBind.init();
	}
};

ms.modal = {
	confirmF: function(){
		var value = $(this).attr('data-value');
		ms.modal.currentHandler.call(this, value);
		$('#modalConfirm').trigger('reveal:close');
	}
	, currentHandler: function(){ }
	, confirm: function(title, handler){
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
	get: function(context, messageSel, msgs){
		ms.ml.html(messageSel, '');
		msgs.SYSTEMERROR = 'A serious error has occurred.  If this continues please contact an administrator.';
		return {
			sendInfo: function(msg){
				$(messageSel).removeClass('secondary alert success');
				this.temp(2, msg);
			}
			, sendError: function(msg){
				this.temp(4, msg);
			}
			, sendWarning: function(msg){
				this.temp(8, msg);
			}
			, msgs: msgs
			, qCtrl: new __queueController({})
			, getMsg: function(status, msg){
				var scope = this;
				var s = this.lookupStatus(status);
				var pid = '{c}_{id}'.bind({ c: context.replace(/[^\w^\d]+/g, '_'), id: msg.replace(/[^\w^\d]+/g, '_') }).toLowerCase();
				var id = '__msg_{id}'.bind({ id: pid });
				var msgMl = '<div id="{id}" class="alert-box {cls} round">{msg}</div>'.bind({ id: id, cls: s.cls, stat: s.title, msg: msg });
				return { 
					send: function(){
						ms.ml.append(messageSel, msgMl);
						var qI = new __queueInfo({
							key: '{c}-PAGEMESSAGE'.bind({ c: context })
							, interval: 500
							, lifespan: 2000
							, onComplete: function () {
								if (s.onFClear){
									$('#' + id).fadeOut('fast', function(){
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
				return ;
			}
			, temp: function(status, msg){
				if (msg.Messages === undefined){
					var m = this.getMsg(status, msg);
					m.send();
				}
				else {
					msg.Messages.select(function(){
						var m = this.getMsg(this.Status, msg);
						m.send();
					});
				}
			}
			, lookupStatus: function(id){
				return this.statususes[id];
			}
			, statususes: { 
				1:  { title: 'System Error', cls: 'alert', onFClear: false }
				, 2: { title: '', cls: 'success', onFClear: true }
				, 4: { title: 'Error', cls: 'alert', onFClear: false }
				, 8: { title: 'Warning', cls: '', onFClear: true }
			}
		}
	}
}
})(window);



