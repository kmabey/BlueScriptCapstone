
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

	});

	function updateChapter() {
		var mytext = ' ';
		$("#chapterSelect option:selected").each(function () {
			mytext = $("#chapterSelect option:selected").text();
			$.ajax({
				url: '/Home/UpdateChapter',
				type: 'POST',
				data: { theID: mytext },
				success: function (r) {
					$("#chapterText").text(r);
				}
			});
			return false;
		});
	};

	function updateBody() {
		var myText = $("#chapterText").text();
		$.ajax({
			url: '/Home/UpdateChapter',
			type: 'POST',
			data: { bodyToSave: mytext },
			success: function (r) {
			}
		});
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