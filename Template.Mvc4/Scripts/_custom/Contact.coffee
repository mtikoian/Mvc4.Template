$ ->
  $("#contactDialog").hide()

  $.getJSON "/contacts/knockoutindex", (jsonData) ->

    viewModel =
      contacts: ko.observableArray(ko.toProtectedObservableItemArray(jsonData))
      contactToAdd: ko.observable("")
      selectedContact: ko.observable(null)
      addContact: ->
        #@contacts.push new ko.protectedObservableItem(Name: @contactToAdd())
        newContact = 
         FirstName: @contactToAdd()
        @contactToAdd ""
        CrudHelpers.ajaxAdd "create", 
         ko.toJSON newContact
         (data) ->
            viewModel.contacts.push (new ko.protectedObservableItem(data))

      selectContact: ->
        viewModel.selectedContact this

    $(document).on "click", ".contact-delete", ->
      itemToRemove = undefined
      itemToRemove = ko.dataFor(this)
      viewModel.contacts.remove itemToRemove

    $(document).on "click", ".contact-edit", ->
      $("#contactDialog").dialog 
        buttons:
          Save: ->
            name = undefined
            viewModel.selectedContact().commit(); 
            $(this).dialog "close"
            CrudHelpers.ajaxUpdate "edit", 
              ko.toJSON viewModel.selectedContact(),
              (data) -> humane data

          Cancel: ->
            $(this).dialog "close",
        width: 500
        title: "Edit Contact"
         

    ko.applyBindings viewModel