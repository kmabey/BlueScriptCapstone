$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $("#openDialog").live("click", function (e) {
        e.preventDefault();
        var url = $(this).attr('href');

        $("#dialog-edit").dialog({
            title: 'Add Student',
            autoOpen: false,
            resizable: false,
            height: 355,
            width: 400,
            show: { effect: 'drop', direction: "up" },
            modal: true,
            draggable: true,
            open: function (event, ui) {
                $(this).load(url);
            },
            close: function (event, ui) {
                $(this).dialog('close');
            }
        });

        $("#dialog-edit").dialog('open');
        return false;
    });

    $(".editDialog").live("click", function (e) {
        var url = $(this).attr('href');
        $("#dialog-edit").dialog({
            title: 'Edit Setting',
            autoOpen: false,
            resizable: false,
            height: 355,
            width: 400,
            show: { effect: 'drop', direction: "up" },
            modal: true,
            draggable: true,
            open: function (event, ui) {
                $(this).load(url);

            },
            close: function (event, ui) {
                $(this).dialog('close');
            }
        });

        $("#dialog-edit").dialog('open');
        return false;
    });

    $(".confirmDialog").live("click", function (e) {

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

    $(".viewDialog").live("click", function (e) {
        var url = $(this).attr('href');
        $("#dialog-view").dialog({
            title: 'View Customer',
            autoOpen: false,
            resizable: false,
            height: 355,
            width: 400,
            show: { effect: 'drop', direction: "up" },
            modal: true,
            draggable: true,
            open: function (event, ui) {
                $(this).load(url);

            },
            buttons: {
                "Close": function () {
                    $(this).dialog("close");

                }
            },
            close: function (event, ui) {
                $(this).dialog('close');
            }
        });

        $("#dialog-view").dialog('open');
        return false;
    });

    $("#btncancel").live("click", function (e) {
        $("#dialog-edit").dialog('close');

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

