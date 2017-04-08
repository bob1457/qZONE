﻿angular.module("angularValidator", []), angular.module("angularValidator").directive("angularValidator", ["$injector", "$parse", function (a, b) { return { restrict: "A", link: function (c, d, e, f) { function g(b) { return b && "invalid-message" in b.attributes ? a.get(b.attributes["invalid-message"].value) : !1 } function h(a) { for (var b = g(a), c = 0; c < a.length; c++) c in a && i(a[c], b) } function i(a, b) { "validate-on" in a.attributes && "blur" === a.attributes["validate-on"].value && angular.element(a).on("blur", function () { m(a, b), p(a) }); var d = c.$watch(function () { return a.value + a.required + t.submitted + l(a) + j(t[a.name]) + k(t[a.name]) }, function () { if (t.submitted) m(a, b), p(a); else { var c = "validate-on" in a.attributes && "dirty" === a.attributes["validate-on"].value; c ? (m(a, b), p(a)) : t[a.name] && t[a.name].$pristine && (m(a, b), p(a)) } }); r.push(d) } function j(a) { return a && "$dirty" in a ? a.$dirty : void 0 } function k(a) { return a && "$valid" in a ? a.$valid : void 0 } function l(a) { if ("validator" in a.attributes) { var b = c.$eval(a.attributes.validator.value); return t[a.name].$setValidity("angularValidator", b), b } } function m(a, b) { var d = function () { return "<i class='fa fa-times'></i> Required" }, e = function () { return "<i class='fa fa-times'></i> Invalid" }; if (a.name in t) { var f = t[a.name], g = o(a); g && g.remove(), (f.$dirty || c[a.form.name] && c[a.form.name].submitted) && (f.$error.required ? "required-message" in a.attributes ? angular.element(a).after(n(a.attributes["required-message"].value)) : angular.element(a).after(n(d)) : f.$valid || ("invalid-message" in a.attributes ? angular.element(a).after(n(a.attributes["invalid-message"].value)) : b ? angular.element(a).after(n(b.message(f, a))) : angular.element(a).after(n(e)))) } } function n(a) { return "<label class='control-label has-error validationMessage'>" + c.$eval(a) + "</label>" } function o(a) { for (var b = angular.element(a).parent().children(), c = 0; c < b.length; c++) if (angular.element(b[c]).hasClass("validationMessage")) return angular.element(b[c]); return !1 } function p(a) { if (a.name in t) { var b = t[a.name]; angular.element(a).removeClass("has-error"), angular.element(a.parentNode).removeClass("has-error"), (b.$dirty || c[a.form.name] && c[a.form.name].submitted) && b.$invalid && (angular.element(a.parentNode).addClass("has-error"), angular.element(a).addClass("has-error")) } } var q = angular.element(d)[0], r = [], s = q.attributes.name.value, t = b(s)(c); t.submitted = !1, c.$watch(function () { return Object.keys(t).length }, function () { angular.forEach(r, function (a) { a() }), h(q) }), d.on("submit", function (a) { a.preventDefault(), c.$apply(function () { t.submitted = !0 }), t.$valid && c.$apply(function () { c.$eval(q.attributes["angular-validator-submit"].value) }) }), t.reset = function () { for (var a = 0; a < q.length; a++) q[a].name && (t[q[a].name].$setViewValue(""), t[q[a].name].$render()); t.submitted = !1, t.$setPristine() }, h(q) } } }]);