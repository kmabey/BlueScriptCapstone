
	$(document).ready(function () {

		$.ajaxSetup({ cache: false });
		$("#accordion").accordion({ collapsible: true });

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
		$('.formHolder').hide();

	});

	function createlink()
	{
		$.ajax({
			url: '/Home/Link/',
			type: 'POST',
			success: function (r) {
				$('#linkholder').html(r);
			}
		});
		return false;
	};

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
			type: 'POST',
			data: $('#sceneForm' + num).serialize(),
			success: function (r) {
				$('#sidrcontent').html(r);
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
				$('#sidrcontent').html(r);
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


	function updateProject(num) {
		$.ajax({
			url: '/Home/EditProject/' + num,
			type: 'POST',
			data: $('#proForm' + num).serialize(),
			success: function (r) {
				$('#sidrcontent').html(r);
				var options = {};
				$('#savePro' + num).show("blind", options, 500, callbackPro(num));
			}
		});
		return false;
	};

	function callbackPro(id) {
		setTimeout(function () {
			var options = {};
			$('#savePro' + id + ':visible').removeAttr("style").fadeOut();
		}, 1000);
	};

	function updateCharacter(num) {
		$.ajax({
			url: '/Home/EditCharacter/' + num,
			type: 'POST',
			data: $('#charForm' + num).serialize(),
			success: function (r) {
				$('#sidrcontent').html(r);
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

	function addProject()
	{
		$.ajax({
			url: '/Home/CreateProject/',
			type: 'GET',
			success: function (r) {
				$('#dialog').html(r);
				$('.hideme').hide();
			}
		});
		return false;
	};

	function addSet() {
		$.ajax({
			url: '/Home/CreateSetting/',
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				$('.hideme').hide();
			}
		});
		return false;
	};

	function addScene() {
		$.ajax({
			url: '/Home/CreateScene/',
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				$('.hideme').hide();
			}
		});
		return false;
	};


	/*function addChapter() {
		$.ajax({
			url: '/Home/CreateChapter/',
			type: 'GET',
			success: function (r) {
				$('#accordion').html(r);
				$('.hideme').hide();
				$('#accordion').accordion({ collapsible: true });
			}
		});
		return false;
	};*/

	function addCharacter() {
		$.ajax({
			url: '/Home/CreateCharacter/',
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				$('.hideme').hide();
			}
		});
		return false;
	};

	function deleteProject(num) {
		$.ajax({
			url: '/Home/DeleteProject/' + num,
			type: 'GET',
			success: function (r) {
				$('#dialog').html(r);
				$('.hideme').hide();
				
			}
		});
		return false;
	};

	function deleteSet(num) {
		$.ajax({
			url: '/Home/DeleteSetting/' + num,
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				$('.hideme').hide();
				var options = {};
				$('#formHolder').toggle('drop', options, 1000);
			}
		});
		return false;
	};

	function deleteScene(num) {
		$.ajax({
			url: '/Home/DeleteScene/' + num,
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				$('.hideme').hide();
				var options = {};
				$('#formHolder').toggle('drop', options, 1000);
			}
		});
		return false;
	};

	function deleteCharacter(num) {
		$.ajax({
			url: '/Home/DeleteCharacter/' + num,
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				$('.hideme').hide();
				var options = {};
				$('#formHolder').toggle('drop', options, 1000);
			}
		});
		return false;
	};

	/*function deleteChapter(num) {
		$.ajax({
			url: '/Home/DeleteChapter/' + num,
			type: 'GET',
			success: function (r) {
				$('#accordion').html(r);
				$('.hideme').hide();
			}
		});
		return false;
	};*/

	function showScene(num)
	{
		$.ajax({
			url: '/Home/ScenePartial/' + num,
			type: 'GET',
			success: function (r) {
				$('#formHolder').html(r);
				var options = {};
				$("#formHolder").toggle('drop', options, 1000);
				$('.hideme').hide();
			}
		});
		return false;
	};

	function showCharacter(num) {
		$.ajax({
			url: '/Home/EditCharacter/' + num,
			type: 'GET',
			success: function (r) {
				$('#formHolder').html(r);
				var options = {};
				$("#formHolder").toggle('drop', options, 1000);
				$('.hideme').hide();
			}
		});
		return false;
	};

	function showSetting(num) {
		$.ajax({
			url: '/Home/EditSetting/' + num,
			type: 'GET',
			success: function (r) {
				$('#formHolder').html(r);
				var options = {};
				$("#formHolder").toggle('drop', options, 1000);
				$('.hideme').hide();
			}
		});
		return false;
	};

	function showSceneNav()
	{
		$.ajax({
			url: '/Home/Scenes',
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				var options = {};
				if ($('#formHolder').is(':visible'))
					$("#formHolder").toggle('drop', options, 1000);
				$("#sidrcontent").toggle('drop', options, 1000);
			}
		});
		return false;
	};

	function showCharacterNav() {
		$.ajax({
			url: '/Home/Characters',
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				var options = {};
				if ($('#formHolder').is(':visible'))
					$("#formHolder").toggle('drop', options, 1000);
				$("#sidrcontent").toggle('drop', options, 1000);
			}
		});
		return false;
	};

	function showSettingNav() {
		$.ajax({
			url: '/Home/Settings',
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				var options = {};
				if ($('#formHolder').is(':visible'))
					$("#formHolder").toggle('drop', options, 1000);
				$("#sidrcontent").toggle('drop', options, 1000);
			}
		});
		return false;
	};

	function showStatsNav() {
		$.ajax({
			url: '/Home/MyStats',
			type: 'GET',
			success: function (r) {
				$('#sidrcontent').html(r);
				var options = {};
				if ($('#formHolder').is(':visible'))
					$("#formHolder").toggle('drop', options, 1000);
				$("#sidrcontent").toggle('drop', options, 1000);
			}
		});
		return false;
	};

	function addC(num)
	{
		$.ajax({
			url: '/Home/AddToC/' + num,
			type: 'GET',
			success: function (r) {
				$('#unsortedTable').html(r);
			}
		});
		return false;
	};

	function addS(num) {
		$.ajax({
			url: '/Home/AddToS/' + num,
			type: 'GET',
			success: function (r) {
				$('#unsortedTable').html(r);
			}
		});
		return false;
	};

	function addI(num) {
		$.ajax({
			url: '/Home/AddToI/' + num,
			type: 'GET',
			success: function (r) {
				$('#unsortedTable').html(r);
			}
		});
		return false;
	};