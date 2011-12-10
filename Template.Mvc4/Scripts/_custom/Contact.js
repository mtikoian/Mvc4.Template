
  $(function() {
    $("#contactDialog").hide();
    return $.getJSON("/contacts/knockoutindex", function(jsonData) {
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
          return CrudHelpers.ajaxAdd("create", ko.toJSON(newContact), function(data) {
            return viewModel.contacts.push(new ko.protectedObservableItem(data));
          });
        },
        selectContact: function() {
          return viewModel.selectedContact(this);
        }
      };
      $(document).on("click", ".contact-delete", function() {
        var itemToRemove;
        itemToRemove = void 0;
        itemToRemove = ko.dataFor(this);
        return viewModel.contacts.remove(itemToRemove);
      });
      $(document).on("click", ".contact-edit", function() {
        return $("#contactDialog").dialog({
          buttons: {
            Save: function() {
              var name;
              name = void 0;
              viewModel.selectedContact().commit();
              $(this).dialog("close");
              return CrudHelpers.ajaxUpdate("edit", ko.toJSON(viewModel.selectedContact(), function(data) {
                return humane(data);
              }));
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
