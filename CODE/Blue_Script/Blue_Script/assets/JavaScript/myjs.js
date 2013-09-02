$(document).ready(function () {

	$.ajaxSetup({ cache: false });

	$('#navigation a').stop().animate({ 'marginLeft': '-85px' }, 1000);

	$("#opensidr").sidr();

	$('#navigation > li').hover(
  function () {
  	$('a', $(this)).stop().animate({ 'marginLeft': '-2px' }, 200);
  },
  function () {
  	$('a', $(this)).stop().animate({ 'marginLeft': '-85px' }, 200);
  }
);

  $("#dialog").dialog({
  	autoOpen: false,
	height: 300,
	modal: true,
  	show: {
  		effect: "blind",
  		duration: 1000
  	},
  	hide: {
  		effect: "explode",
  		duration: 1000
  	},
  	buttons: {
  		"Okay": function () {
  			$(this).dialog("close");
  		},
  		Cancel: function () {
  			$(this).dialog("close");
  		}
  	}
  });

  $(".projectNav").click(function () {
  	$("#dialog").dialog("open");
  });


});


var dragSrcEl = null;
function handleDragStart(e) 
{

    dragSrcEl = this;

    e.dataTransfer.effectAllowed = 'move';
    e.dataTransfer.setData('text/html', this.innerHTML);
}

//Called whenever you are on top of an object with this event.
function handleDragOver(e) 
{

    //WARNING: You must have this if statement or the drop event will not be called.
    if (e.preventDefault) e.preventDefault();

    e.dataTransfer.dropEffect = 'move';  

    return false;
}

//Called when object enters the bounds of another object.
function handleDragEnter(e) 
{
    this.classList.add('over');
}

//Called when you leave a object that has this event.
function handleDragLeave(e) 
{
    this.classList.remove('over');
}

//Called when you drop on top of an object with this event.
var counter = 1;
function handleDrop(e) 
{
    if (e.stopPropagation) e.stopPropagation();

    if (dragSrcEl != this) 
    {
        dragSrcEl.innerHTML = this.innerHTML;
        this.innerHTML = e.dataTransfer.getData('text/html');
    }

    return false;
}

//Called when you stop dragging(release) an object.
function handleDragEnd(e) 
{
    [ ].forEach.call(objects, function (object) 
    {
        object.classList.remove('over');
    });
}

var objects = document.querySelectorAll('[draggable=true]');
[ ].forEach.call(objects, function (object) 
{
    object.addEventListener('dragstart', handleDragStart, false);
    object.addEventListener('dragenter', handleDragEnter, false);
    object.addEventListener('dragover', handleDragOver, false);
    object.addEventListener('dragleave', handleDragLeave, false);
    object.addEventListener('drop', handleDrop, false);
    object.addEventListener('dragend', handleDragEnd, false);
});

