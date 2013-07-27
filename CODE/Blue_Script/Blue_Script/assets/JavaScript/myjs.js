$(document).ready(function () 
{
    element('bin').addEventListener('dragover', handleDragOver, false);
    element('bin').addEventListener('drop', handleDrop, false);

    $(".btn.btn-mini.btn-danger").live('click', function () 
    {
        $(this).parent().parent().remove();
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

    text('count: ' + counter++);

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

