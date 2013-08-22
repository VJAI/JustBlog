
$(function () {

  $('#search-form').submit(function () {
    if ($("#s").val().trim())
      return true;
    return false;
  });
  
});
