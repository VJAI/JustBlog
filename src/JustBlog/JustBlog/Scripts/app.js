
$(function () {

  $('#search-form').submit(function () {
    if ($("#search").val().trim())
      return true;
    return false;
  });
  
});