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
      $("#contactDialog").dialog buttons:
        Save: ->
          name = undefined
          $(this).dialog "close"
          viewModel.selectedContact().commit()

        Cancel: ->
          $(this).dialog "close"

    ko.applyBindings viewModel