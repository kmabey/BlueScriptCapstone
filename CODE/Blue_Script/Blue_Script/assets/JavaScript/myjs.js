﻿$(document).ready(function () {
	$(".simpledialog").click(function () {
		InitializeDialog($('.dialogbox'));

		$('.dialogbox').dialog('open');
	});

	function InitializeDialog($element) {


		$element.dialog({
			autoOpen: false,
			width: 400,
			resizable: true,
			draggable: true,
			title: "Test Dialog",
			model: true,
			show: 'slide',
			closeText: 'x',
			dialogClass: 'alert',
			closeOnEscape: true,
			open: function (event, ui) {
				//Load the Partial View Here using Controller and Action
				$element.load('/Home/EditCharacter');
			},

			close: function () {
				$(this).dialog('close');
			}

		});

	}
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

