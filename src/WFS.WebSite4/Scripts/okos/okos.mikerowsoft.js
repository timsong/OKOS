/// <reference path="jquery-1.5.1-vsdoc.js" />
(function (window) {
	uti = {
		getRnd: function (max) {
			var rdn = Math.floor(Math.random() * max + 1);
			return rdn;
		}
	};
	/** extending **/
	Function.prototype._thread = function (milliseconds) {
		setTimeout(this.call(this), milliseconds);
	}
	Function.prototype._start = function (milliseconds, condition) {
		return setInterval(this, milliseconds);
	}
	Function.prototype._while = function (milliseconds, condition) {
		var s = this;
		if (condition.call() == true) {
			s.call(this);
			setTimeout(function () {
				s._while(milliseconds, condition);
			}, milliseconds);
		}
	}
	String.prototype.contains = function (text) {
		return this.indexOf(text) >= 0;
	}


	String.format = function (tmp) {
		var args = arguments;
		return tmp.replace(/{(\d+)}/g, function (match, number) {
			return typeof args[number] != 'undefined'
      ? args[number]
      : match
    ;
		});
	};
	String.prototype.format = function () {
		var args = arguments;
		return this.replace(/{(\d+)}/g, function (match, number) {
			return typeof args[number] != 'undefined'
      ? args[number]
      : match
    ;
		});
	};
	String.prototype.bind = function (subject) {
		var s = this;
		var rex = /[\{][^\}]+[\}]/;
		var idx = 0;
		while (rex.test(s)) {
			var result = rex.exec(s);
			if (result) {
				var r = result[0];
				var ref = r.replace(/[\{\}]+/g, '');
				var val = '';
				if (typeof subject[ref] != 'undefined') {
					val = (subject[ref] === 'function') ? subject[ref]() : subject[ref];
				}
				else {
					try {
						var e = eval(ref);
						val = e === 'function' ? e() : e;
					}
					catch (ex) {
						throw new Error('Badly named binding ' + ref);
					}
				}
				s = s.replace(r, val);
			}
		}
		return s;
	}
	String.prototype.splitByUCase = function () {
		return this.replace(/([A-Z])/g, ' $1');
	}
	window.parseBool = function (str) {
		return str.toString().toLowerCase() == 'true';
	}
	Number.prototype.between = function (a, b) {
		if (typeof a == 'number') {
			return this >= a && this <= b;
		}
		return false;
	};
	Number.prototype._for = function (a, b) {
		if (typeof a == 'number') {
			for (var i = this; i < a; i++) { b.call(this, i); }
		}
		else {
			for (var i = 0; i < this; i++) { a.call(this, i); }
		}
	};
	Date.prototype.clone = function () { return new Date(this.getYear(), this.getMonth(), this.getDate(), this.getHours(), this.getMinutes(), this.getSeconds(), this.getMilliseconds()); }
	Date.prototype.firstDay = function () { this.setDate(1); return this; }
	Date.prototype.addMonth = function () { this.setMonth(this.getMonth() + 1); return this; }
	Date.prototype.lastDay = function () { this.setMonth(0); return this; }
	Date.prototype.addDay = function (day) { this.setDate(day); return this; }
	Date.prototype.getHour = function () { var hour = this.getHours(); return hour == 0 ? 12 : hour > 12 ? hour - 12 : hour; }
	Date.prototype.getMinute = function () { return this.getMinutes(); }
	Date.prototype.isValid = function () {
		return !isNaN(this.getDate()) || !isNaN(this.getMonth()) || !isNaN(this.getYear()) || !(this.getDate() > 31) || !(this.getYear() < 1900) || !(this.getMonth() > 12);
	}
	Date.prototype.setPeriod = function (period) {
		if (this.getPeriod() != period) {
			if (this.getPeriod() == 'AM') {
				this.setHours(this.getHours() + 12);
			}
			else {
				this.setHours(this.getHours() - 12);
			}
		}
		return this;
	}
	Date.prototype.getPeriod = function () {
		return this.getHours() > 11 ? 'PM' : 'AM';
	}
	Date.fromJson = function (date) {
		return eval('new ' + date.replace(/\//g, ''));
	}

	Date.prototype.getMonthInfo = function () {
		var firstDay = this.clone().firstDay();
		var lastDay = this.clone().addMonth().firstDay().addDay(0);
		var weeks = Math.ceil(((firstDay.getDay() - 1) + lastDay.getDate()) / 7);
		return { firstDay: firstDay, lastDay: lastDay, weeks: weeks };
	}


	Array.prototype.count = function (action) {
		var result = 0;
		for (var i = 0; i < this.length; i++) {
			if (action && action.call(this[i]))
				result++;
			else if (!action) {
				result++;
			}
		}
		return result;
	}
	Array.prototype.where = function (action) {
		var arr = [];
		for (var i = 0; i < this.length; i++) {
			if (action.call(this[i]))
				arr.push(this[i]);
		}
		return arr;
	}
	Array.prototype.first = function (action) {
		var arr = [];
		if (action) {
			for (var i = 0; i < this.length; i++) {
				if (action.call(this[i]))
					arr.push(this[i]);
			}
			return arr.length > 0 ? arr[0] : null;
		}
		return this.length > 0 ? this[0] : null;
	}
	Array.prototype.select = function (action) {
		var arr = [];
		for (var i = 0; i < this.length; i++) {
			arr.push(action.call(this[i]));
		}
		return arr;
	};
	Array.prototype._select = function (action) {
		var arr = [];
		for (var i = 0; i < this.length; i++) {
			arr.push(action.call(this[i]));
		}
		return arr;
	};
	Array.prototype.distinct = function () {
		var result = [];
		for (var i = 0; i < this.length; i++) {
			if (!result.contains(this[i])) {
				result.push(this[i]);
			}
		}
		return result;
	};

	Array.prototype.sum = function (action) {
		var result = 0;
		for (var i = 0; i < this.length; i++) {
			result += action ? parseInt(action.call(this[i])) : parseInt(this[i]);
		}
		return result;
	};

	Array.prototype.contains = function (value) {
		for (key in this) {
			if (this[key] == value) {
				return true;
			}
		}
		return false;
	}
	$.expr[':'].noCls = function (obj, index, meta, stack) {
		var cls, antiCls = [];
		var m = meta[3].split(',');
		if (m.length > 1) {
			cls = m[0].split(' ').where(function () { return this.length > 0; });
			antiCls = m[1].split(' ');
		}
		else {
			cls = m[0].split(' ');
		}
		var cF = cls.length > 0 ? false : true;
		var aF = false;
		$(cls).each(function (idx) {
			if ($(obj).hasClass(this)) cF = true;
		});
		$(antiCls).each(function (idx) {
			if ($(obj).hasClass(this)) aF = true;
		});
		return cF && !aF;
	};


})(window);
