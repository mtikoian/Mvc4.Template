$ ->
  $("#contactDialog").hide()
  baseUrl = "/api/contacts"
  $.getJSON baseUrl, (jsonData) ->

    viewModel =
      contacts: ko.observableArray(ko.toProtectedObservableItemArray(jsonData))
      contactToAddFirst: ko.observable("")
      contactToAddLast: ko.observable("")
      selectedContact: ko.observable(null)
      addContact: ->
        newContact = 
         FirstName: @contactToAddFirst()
         LastName: @contactToAddLast()
        @contactToAddFirst ""
        @contactToAddLast ""
        CrudHelpers.ajaxAdd baseUrl + "", 
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
            viewModel.selectedContact().commit()
            currentContact = viewModel.selectedContact()
            currentContact.ModifyDate = undefined
            contactId = currentContact.Id()
            $(this).dialog "close"
            CrudHelpers.ajaxUpdate baseUrl + "/" + contactId,  
              (ko.toJSON currentContact), 
              (data) ->
                humane "Contact Id: " + data.Id + " updated."
                

          Cancel: ->
            $(this).dialog "close",
        width: 500
        title: "Edit Contact"
         

    ko.applyBindings viewModel