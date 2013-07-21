function doHighlight(bodyText, searchTerm) {

    var highlightStartTag = "<font style=color:blue; background-color:yellow;>";
    var highlightEndTag = "</font>";

  var newText = "";
  var i = -1;
  var lcSearchTerm = searchTerm.toLowerCase();
  var lcBodyText = bodyText.toLowerCase();
    
  while (bodyText.length > 0) {
    i = lcBodyText.indexOf(lcSearchTerm, i+1);
    if (i < 0) {
      newText += bodyText;
      bodyText = "";
    } else {
          newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
          bodyText = bodyText.substr(i + searchTerm.length);
          lcBodyText = bodyText.toLowerCase();
          i = -1;
        }
      }
        return newText;
    }
  


function highlightSearchWord(searchText) {

  if (!document.body || typeof(document.body.innerHTML) == "undefined") {
      alert("Sorry, for some reason the text of this page is unavailable. Searching will not work.");
    return false;
  }
  
  var bodyText = document.body.innerHTML;
    bodyText = doHighlight(bodyText, searchText);
  
  document.body.innerHTML = bodyText;
  return true;
}


function searchPrompt(defaultText)
{   
    var promptText = "Please enter the word you would like to search for:";
  
    var searchText = prompt(promptText, defaultText);

  if (!searchText)  {
    alert("No input was entered. No highlighting will be done");
    return false;
  }
  
  return highlightSearchWord(searchText);
}
