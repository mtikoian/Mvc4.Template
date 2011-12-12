$ ->
  $("#contactDialog").hide()
  baseUrl = "/api/contacts"
  $.getJSON baseUrl, (jsonData) ->

    viewModel =
      contacts: ko.observableArray(ko.toProtectedObservableItemArray(jsonData))
      contactToAdd: ko.observable("")
      selectedContact: ko.observable(null)
      addContact: ->
        newContact = 
         FirstName: @contactToAdd()
        @contactToAdd ""
        CrudHelpers.ajaxAdd baseUrl, 
         ko.toJSON newContact
         (data) ->
            viewModel.contacts.push (new ko.protectedObservableItem(data))

      selectContact: ->
        viewModel.selectedContact this

    $(document).on "click", ".contact-delete", ->
      itemToRemove = ko.dataFor(this)
      CrudHelpers.ajaxDelete baseUrl, 
        viewModel.selectedContact().Id().toString(),
        (data) -> 
          viewModel.contacts.remove itemToRemove          
          humane data
          

    $(document).on "click", ".contact-edit", ->
      $("#contactDialog").dialog 
        buttons:
          Save: ->
            name = undefined
            viewModel.selectedContact().commit(); 
            $(this).dialog "close"
            CrudHelpers.ajaxUpdate baseUrl + "/" + viewModel.selectedContact().Id().toString(),  
              (ko.toJSON viewModel.selectedContact()), 
              (data) ->
                humane "Contact Id: " + data.Id + " updated."

          Cancel: ->
            $(this).dialog "close",
        width: 500
        title: "Edit Contact"
         

    ko.applyBindings viewModel