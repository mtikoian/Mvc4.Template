$ -> 

  $.ajaxSetup cache: false
  window.CrudHelpers =
    ajaxAdd: (url, dataToSave, callback) ->
      this.ajaxModify url, dataToSave, "POST", "Tag added.", callback

    ajaxUpdate: (url, dataToSave, successCallback) ->
      ajaxModify url, dataToSave, "PUT", "Tag updated.", successCallback

    ajaxDelete: (url) ->
      ajaxModify url, null, "DELETE", "Tag Deleted.", successCallback

    ajaxModify: (url, dataToSave, httpVerb, successMessage, callback) ->
      $.ajax url, 
         data: dataToSave, 
         type: httpVerb, 
         dataType: 'json', 
         contentType: 'application/json',
         success: (data) ->  
           humane successMessage
           callback data  
         error: ->
           humane "an error occured"
  
