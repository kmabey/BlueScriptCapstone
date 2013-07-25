$(document).ready(function () 
{

});

var dragSrcEl = null;
function handleDragStart(e) 
{

    dragSrcEl = this;

    e.dataTransfer.effectAllowed = 'move';
    e.dataTransfer.setData('text/html', this.innerHTML);
}

function handleDragOver(e) 
{
    if (e.preventDefault) 
    {
        e.preventDefault(); 
    }

    e.dataTransfer.dropEffect = 'move';  

    return false;
}

function handleDragEnter(e) 
{
    this.classList.add('over');
}

function handleDragLeave(e) 
{
    this.classList.remove('over');
}

function handleDrop(e) 
{
    if (e.stopPropagation) 
    {
        e.stopPropagation();
    }

    if (dragSrcEl != this) 
    {
        dragSrcEl.innerHTML = this.innerHTML;
        this.innerHTML = e.dataTransfer.getData('text/html');
    }

    return false;
}

function handleDragEnd(e) 
{
    [ ].forEach.call(events, function (ev) 
    {
        ev.classList.remove('over');
    });
}

var events = document.querySelectorAll('[draggable=true]');
[ ].forEach.call(events, function (ev) 
{
    ev.addEventListener('dragstart', handleDragStart, false);
    ev.addEventListener('dragenter', handleDragEnter, false);
    ev.addEventListener('dragover', handleDragOver, false);
    ev.addEventListener('dragleave', handleDragLeave, false);
    ev.addEventListener('drop', handleDrop, false);
    ev.addEventListener('dragend', handleDragEnd, false);
});

var bin = document.getElementById('bin');
bin.addEventListener('drop', function (e) 
{
    var file_name = e.dataTransfer.getData('Text'),
        file = document.querySelector('.file[data-name="' + file_name + '"]');

    //If the file exists...
    if (file) 
    {
        //We change the message
        document.getElementById('results').innerHTML = 'The file <em>' + file_name + '</em> has been deleted!';

        //We remove the file from the DOM
        file.parentNode.removeChild(file);

        //We add the "full" class to the bin in order to change its background
        addClass(this, 'full');
    }

    //And we prevent the default action!					
    if (e.preventDefault) { e.preventDefault(); }
}, false);