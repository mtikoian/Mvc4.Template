
  $(function() {
    $.ajaxSetup({
      cache: false
    });
    return window.CrudHelpers = {
      ajaxAdd: function(url, dataToSave, callback) {
        return this.ajaxModify(url, dataToSave, "POST", "Tag added.", callback);
      },
      ajaxUpdate: function(url, dataToSave, successCallback) {
        dataToSave.ModifyDate = new Date();
        return this.ajaxModify(url, dataToSave, "POST", "Tag updated.", successCallback);
      },
      ajaxDelete: function(url) {
        return this.ajaxModify(url, null, "DELETE", "Tag Deleted.", successCallback);
      },
      ajaxModify: function(url, dataToSave, httpVerb, successMessage, callback) {
        return $.ajax(url, {
          data: dataToSave,
          type: httpVerb,
          dataType: 'json',
          contentType: 'application/json',
          success: function(data) {
            humane(successMessage);
            return callback(data);
          },
          error: function() {
            return humane("an error occured");
          }
        });
      }
    };
  });
