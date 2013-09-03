
	$(document).ready(function () {

		//Start of test code.
		$.ajaxSetup({ cache: false });
		$("#accordion").accordion({ collapsible: true });

		function runEffect1() {
			var options = {};
			$("#tabs-1").toggle('clip', options, 1000);
		};

		$("#sceneHead").click(function () {
			runEffect1();
			return false;
		});
		function runEffect2() {
			var options = {};
			$("#tabs-2").toggle('clip', options, 1000);
		};

		$("#charHead").click(function () {
			runEffect2();
			return false;
		});
		function runEffect3() {
			var options = {};
			$("#tabs-3").toggle("clip", options, 1000);
		};

		$("#setHead").click(function () {
			runEffect3();
			return false;
		});

		function runEffect4() {
			var options = {};
			$("#sidrcontenta").toggle("drop", options, 1000);
		};

		$(".sceneNav").click(function () {
			runEffect4();
			return false;
		});

		function runEffect5() {
			var options = {};
			$("#sidrcontentb").toggle("drop", options, 1000);
		};

		$(".characterNav").click(function () {
			runEffect5();
			return false;
		});

		function runEffect6() {
			var options = {};
			$("#sidrcontentc").toggle("drop", options, 1000);
		};

		$(".settingNav").click(function () {
			runEffect6();
			return false;
		});

		function runEffect7() {
			var options = {};
			$("#sidrcontentd").toggle("drop", options, 1000);
		};

		$(".statNav").click(function () {
			runEffect7();
			return false;
		});

		$(".confirmDialog").on("click", function (e) {

			var url = $(this).attr('href');
			$("#dialog-confirm").dialog({
				autoOpen: false,
				resizable: false,
				height: 170,
				width: 350,
				show: { effect: 'drop', direction: "up" },
				modal: true,
				draggable: true,
				buttons: {
					"OK": function () {
						$(this).dialog("close");
						window.location = url;

					},
					"Cancel": function () {
						$(this).dialog("close");

					}
				}
			});
			$("#dialog-confirm").dialog('open');
			return false;
		});

		$("#btncancel").on("click", function (e) {
			$("#dialog-edit").dialog('close');

		});


		$('.hideme').hide();
		$('.sidrcontent').hide();

	});

	function updateChapter(num) {
		$.ajax({
			url: '/Home/EditChapter/' + num,
			type: 'POST',
			data: $('#chapForm' + num).serialize(),
			success: function (r) {
				//var response = jQuery.parseJSON(r);
				$('#chap' + num).html(r.Name);
				var options = {};
				$('#saveChap' + num).show("blind", options, 500, callbackChap(num));
			}
		});
		return false;
	};

	function callbackChap(id) {
		setTimeout(function () {
			var options = {};
			$('#saveChap' + id + ':visible').removeAttr("style").fadeOut();
		}, 1000);
	};


	function updateScene(num) {
		$.ajax({
			url: '/Home/EditScene/' + num,
			type: 'GET',
			data: $('#sceneForm' + num).serialize(),
			success: function (r) {
				var options = {};
				$('#saveScene' + num).show("blind", options, 500, callbackScene(num));
			}
		});
		return false;
	};


	function callbackScene(id) {
		setTimeout(function () {
			var options = {};
			$('#saveScene' + id + ':visible').removeAttr("style").fadeOut();
		}, 1000);
	};

	function updateSetting(num) {
		$.ajax({
			url: '/Home/EditSetting/' + num,
			type: 'POST',
			data: $('#setForm' + num).serialize(),
			success: function (r) {
				var options = {};
				$('#saveSet' + num).show("blind", options, 500, callbackSet(num));
			}
		});
		return false;
	};


	function callbackSet(id) {
		setTimeout(function () {
			var options = {};
			$('#saveSet' + id + ':visible').removeAttr("style").fadeOut();
		}, 1000);
	};

	function updateCharacter(num) {
		$.ajax({
			url: '/Home/EditCharacter/' + num,
			type: 'POST',
			data: $('#charForm' + num).serialize(),
			success: function (r) {
				var options = {};
				$('#saveChar' + num).show("blind", options, 500, callbackChar(num));
			}
		});
		return false;
	};


	function callbackChar(id) {
		setTimeout(function () {
			var options = {};
			$('#saveChar' + id + ':visible').removeAttr("style").fadeOut();
		}, 1000);
	};