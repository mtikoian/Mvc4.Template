
  $(function() {
    var baseUrl;
    $("#contactDialog").hide();
    baseUrl = "/api/contacts";
    return $.getJSON(baseUrl, function(jsonData) {
      var viewModel;
      viewModel = {
        contacts: ko.observableArray(ko.toProtectedObservableItemArray(jsonData)),
        contactToAdd: ko.observable(""),
        selectedContact: ko.observable(null),
        addContact: function() {
          var newContact;
          newContact = {
            FirstName: this.contactToAdd()
          };
          this.contactToAdd("");
          return CrudHelpers.ajaxAdd(baseUrl, ko.toJSON(newContact), function(data) {
            return viewModel.contacts.push(new ko.protectedObservableItem(data));
          });
        },
        selectContact: function() {
          return viewModel.selectedContact(this);
        }
      };
      $(document).on("click", ".contact-delete", function() {
        var itemToRemove;
        itemToRemove = ko.dataFor(this);
        return CrudHelpers.ajaxDelete(baseUrl, viewModel.selectedContact().Id().toString(), function(data) {
          viewModel.contacts.remove(itemToRemove);
          return humane(data);
        });
      });
      $(document).on("click", ".contact-edit", function() {
        return $("#contactDialog").dialog({
          buttons: {
            Save: function() {
              var name;
              name = void 0;
              viewModel.selectedContact().commit();
              $(this).dialog("close");
              return CrudHelpers.ajaxUpdate(baseUrl + "/" + viewModel.selectedContact().Id().toString(), ko.toJSON(viewModel.selectedContact()), function(data) {
                return humane("Contact Id: " + data.Id + " updated.");
              });
            },
            Cancel: function() {
              return $(this).dialog("close");
            }
          },
          width: 500,
          title: "Edit Contact"
        });
      });
      return ko.applyBindings(viewModel);
    });
  });
