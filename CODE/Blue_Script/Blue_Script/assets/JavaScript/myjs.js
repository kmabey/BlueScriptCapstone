$(document).ready(function () 
{

});

function handleDragStart(e) 
{
    this.style.opacity = '0.4';  
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

var cols = document.querySelectorAll('#event .event');
[ ].forEach.call(cols, function (col) 
{
    col.addEventListener('dragstart', handleDragStart, false);
    col.addEventListener('dragenter', handleDragEnter, false);
    col.addEventListener('dragover', handleDragOver, false);
    col.addEventListener('dragleave', handleDragLeave, false);
});