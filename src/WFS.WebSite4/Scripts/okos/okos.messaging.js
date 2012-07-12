/// <reference path="jquery-1.4.1-vsdoc.js" />
(function (window) {
function __msg(config)
{
	this.constructor(config);
	this.init();
}
__msg.prototype = {
	titleMaxLength: 25
	, bodyMaxLength: 100
	, constructor: function (config) {
		for (key in config) {
			this[key] = config[key];
		}
	}
	, expandOnError: false
	, TEMPLATES:
	{
		CONTAINER: '<div id="__msg_container"><div id="__msg_title"></div><div id="__msg_body"></div></div>'
		, MSG_WARNING: '<div id="{0}"><div id="__title_{0}">{1}</div><div id="__body_{0}">{2}</div><div id="__full_{0}" style="display:none">{3}</div></div>'
		, MSG_INFO: '<div id="{0}"><div id="__title_{0}"></div><div id="__body_{1}"></div></div>'
		, MSG_ERROR: '<div id="{0}"><div id="__title_{0}"></div><div id="__body_{1}"></div></div>'
		, MSG_TEASER: '<div id="{0}">TEASER: {1}</div>'
		, MSG_ID: '__msg_{0}'
	}
	, position: 'bottom'
	, POSITIONS:
	{
		TOP: 'top'
		, BOTTOM: 'bottom'
	}
	, posTop: function () {
		this.position = this.POSITIONS.TOP;
		this.reposition();
	}
	, posBottom: function () {
		this.position = this.POSITIONS.BOTTOM;
		this.reposition();
	}
	, reposition: function () {
		if (this.position == this.POSITIONS.TOP) {
			$('#__msg_container').css('top', '10px');
			$('#__msg_container').css('left', '10px');
		}
		else if (this.position == this.POSITIONS.BOTTOM) {
			$('#__msg_container').css('top', ($(window).height()) - $('#__msg_container').height() - 10);
			$('#__msg_container').css('left', '10px');
		}
	}

	, MSGTYPES:
	{
		WARNING: 'warning'
		, INFO: 'info'
		, ERROR: 'error'
		, FATAL: 'fatal'
		, ALL: 'all'
	}
	, prepareContainer: function () {
		$(document.body).append(String.format(this.getTemplate(this.MSGTYPES.ALL, 'CONTAINER'), '', '__msg_title', '__msg_body'));
		$('#__msg_body').css('height', '270px');
		$('#__msg_body').css('overflow', 'auto');
		$('#__msg_title').click(function () {
			Msg.toggleContainer();
		});
		$('#__msg_body').keyup(function () {
			if (event.keyCode == 27) {
				Msg.toggleContainer();
			}
		});
		$('#__msg_floater').keyup(function () {
			if (event.keyCode == 27) {
				Msg.closeFull();
				$('#__msg_floater').focus();
			}
		});

		$(document.body).append(this.getTemplate(this.MSGTYPES.ALL, 'FLOATER'));
		this.reposition();
	}
	, switcher: 'off'
	, bigCls: '__msg_big'
	, smallCls: '__msg_small'
	, turnOnContainer: function () {
		$('#__msg_container').addClass(this.bigCls);
		$('#__msg_container').removeClass(this.smallCls);
		this.reposition();
		$('#__msg_container').focus();
		$('#__msg_body').show();
		this.switcher = 'on';
	}
	, turnOffContainer: function () {
		$('#__msg_container').removeClass(this.bigCls);
		$('#__msg_container').addClass(this.smallCls);
		this.reposition();
		$('#__msg_body').hide();
		this.switcher = 'off';
		var qI = new __queueInfo({
			key: "PAGEMESSAGE"
			, interval: 500
			, lifespan: 3000
			, onComplete: function () {
				Msg.hide();
			}
		});
		if (this.qCtrl == null) {
			this.qCtrl = new __queueController({});
		}
		this.qCtrl.queue(qI);
	}
	, qCtrl: null
	, toggleContainer: function () {
		if (this.switcher == 'on') {
			this.turnOffContainer();
		}
		else {
			this.turnOnContainer();
		}
	}
	, initializePage: function () {
		$('body').scroll(function () {
			Msg.reposition();
		});

		$(window).resize(function () {
			Msg.reposition();
		});
	}
	, getRnd: function () {
		var rn = Math.floor(Math.random() * 111111);
		return rn;
	}
	, messages: new Array()
	, currentId: -1
	, selectMessage: function (msgInfo) {
		if ($.isFunction(this.onSelectMessage)) {
			this.onSelectMessage.call(this, msgInfo);
		}
	}
	, loadFull: function (msgInfo) {
		var msg = String.format(this.getTemplate(msgInfo.msgType, 'FULL'), msgInfo.id, msgInfo.msgTitle, msgInfo.msgBody);
		$('#__msg_floater_body').html(msg);
		$('#__msg_floater').show();
		$('#__msg_floater').focus();
		if ($(msgInfo.selector).length == 0) {
			$('#__msg_floater').css('top', 200);
			$('#__msg_floater').css('left', 150);
		}
		else {
			$('#__msg_floater').css('top', $(msgInfo.selector).position().top + 5);
			$('#__msg_floater').css('left', $(msgInfo.selector).position().left + 10);
		}
	}
	, onSelectMessage: function (msgInfo) {
		this.loadFull(msgInfo);
	}
	, closeFull: function () {
		$('#__msg_floater').hide();
	}
	, msgCount: 0
	, buildMsg: function (selector, msgType, msgTitle, msgBody) {
		this.currentId = this.getRnd();
		var msgId = String.format(this.TEMPLATES.MSG_ID, this.currentId);
		var msgSel = String.format('#{0}', msgId);
		this.messages[msgId] = { id: this.currentId, msgId: msgId, msgType: msgType, msgTitle: msgTitle, msgBody: msgBody, selector: selector };

		var msg = String.format(this.getTemplate(msgType, 'MESSAGE'), msgId, this.getSummary(msgTitle, this.titleMaxLength), this.getSummary(msgBody), msgBody);
		var isfirst = $('.__msg').length == 0;
		$('#__msg_body').append(String.format('{0}{1}', msg));
		if (!isfirst) $(msgSel).insertBefore('.__msg:first');
		$(msgSel).slideDown('fast');
		$(String.format('#{0}', msgId)).click(function () {
			var msgInfo = Msg.messages[msgId];
			Msg.selectMessage(msgInfo);
		});
		this.msgCount++;
		if (this.switcher == 'off') this.turnOffContainer();
		if (this.expandOnError && (msgType == this.MSGTYPES.ERROR || msgType == this.MSGTYPES.FATAL)) this.turnOnContainer();
		this.countMsg();
	}
	, countMsg: function () {
		$('#__msg_count').html(String.format('{0}', this.msgCount));
	}
	, loadTemplate: function (snippet, templateName, type) {
		$.get(snippet + '?rn=' + this.getRnd(), null, function (snippet) {
			if (!$.isArray(Msg.TEMPLATES[templateName.toUpperCase()])) {
				Msg.TEMPLATES[templateName.toUpperCase()] = new Array();
			}
			Msg.TEMPLATES[templateName.toUpperCase()][type] = snippet;
		});
	}
	, loadAllFor: function (file, name) {
		for (key in this.MSGTYPES) {
			if (key == 'ALL') this.loadTemplate(String.format('/Scripts/messaging/snippet_{0}.htm', file, key), name, this.MSGTYPES.ALL);
			else if (key == 'WARNING' || key == 'ERROR' || key == 'FATAL' || key == 'INFO') this.loadTemplate(String.format('/Scripts/messaging/snippet_{0}_{1}.htm', file, key), name, this.MSGTYPES[key]);
		}
	}
	, loadTemplates: function () {
		this.loadAllFor('teaser', 'TEASER');
		this.loadAllFor('message', 'MESSAGE');
		this.loadAllFor('full', 'FULL');
		this.loadTemplate('/Scripts/messaging/snippet_container.htm', 'CONTAINER', this.MSGTYPES.ALL);
		this.loadTemplate('/Scripts/messaging/snippet_floater.htm', 'FLOATER', this.MSGTYPES.ALL);
	}
	, getTemplate: function (type, name) {
		return this.TEMPLATES[name][type] ? this.TEMPLATES[name][type] : this.TEMPLATES[name][this.MSGTYPES.ALL];
	}
	, init: function () {
		this.loadTemplates();
	}
	, getSummary: function (msg, length) {
		if (!length) length = this.bodyMaxLength;
		if (msg.length > length) {
			msg = msg.substring(0, length) + '...';
		}
		return msg;
	}
	, hide: function () {
		if (this.switcher != 'on') {
			$('#__msg_container').fadeOut('fast');
		}
	}
	, show: function () {
		if ($('#__msg_container').length == 0) {
			Msg.prepareContainer.call(Msg);
		}
		else {
			$('#__msg_container').fadeIn('fast');
		}
	}
	, alert: function (selector, msgType, msgTitle, msgBody) {
		this.show();
		$('#__msg_title').html(String.format(this.getTemplate(msgType, 'TEASER'), this.getSummary(msgTitle, this.titleMaxLength)));
		this.buildMsg(selector, msgType, msgTitle, msgBody);
		if (msgType == this.MSGTYPES.FATAL) throw new Error(msgBody);
	}
}
var Msg = new __msg({});
var idx = 1;

$(document).ready(function ()
{
	Msg.initializePage();
});

})(window);
