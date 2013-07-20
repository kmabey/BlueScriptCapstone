function doHighlight(bodyText, searchTerm, highlightStartTag, highlightEndTag) 
{
  // the highlightStartTag and highlightEndTag parameters are optional
  if ((!highlightStartTag) || (!highlightEndTag)) {
    highlightStartTag = "<font style='color:blue; background-color:yellow;'>";
    highlightEndTag = "</font>";
  }
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
      // skip anything inside an HTML tag
      if (bodyText.lastIndexOf(">", i) >= bodyText.lastIndexOf("<", i)) {
        // skip anything inside a <script> block
        if (lcBodyText.lastIndexOf("/script>", i) >= lcBodyText.lastIndexOf("<script", i)) {
          newText += bodyText.substring(0, i) + highlightStartTag + bodyText.substr(i, searchTerm.length) + highlightEndTag;
          bodyText = bodyText.substr(i + searchTerm.length);
          lcBodyText = bodyText.toLowerCase();
          i = -1;
        }
      }
    }
  }
  
  return newText;
}

function highlightSearchWord(searchText, highlightStartTag, highlightEndTag)
{
  if (!document.body || typeof(document.body.innerHTML) == "undefined") {
      alert("Sorry, for some reason the text of this page is unavailable. Searching will not work.");
    return false;
  }
  
  var bodyText = document.body.innerHTML;
  for (var i = 0; i < searchArray.length; i++) {
    bodyText = doHighlight(bodyText, searchArray[i], highlightStartTag, highlightEndTag);
  }
  
  document.body.innerHTML = bodyText;
  return true;
}


function searchPrompt(defaultText)
{
  if (!defaultText) 
  {
    defaultText = "";
  }
    
    highlightStartTag = "";
    highlightEndTag = "";
    
    promptText = "Please enter the word you would like to search for:";
  
  searchText = prompt(promptText, defaultText);

  if (!searchText)  {
    alert("No input was entered. No highlighting will be done");
    return false;
  }
  
  return highlightSearchWord(searchText, true, highlightStartTag, highlightEndTag);
}
