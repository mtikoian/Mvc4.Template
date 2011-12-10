//http://www.knockmeout.net/2011/03/guard-your-model-accept-or-cancel-edits.html

//wrapper to an observable that requires accept/cancel
$(function() {
   ko.protectedObservable = function(initialValue) {
      var result, _actualValue, _tempValue;
      _actualValue = ko.observable(initialValue);
      _tempValue = initialValue;
      result = ko.dependentObservable({
            read: function() {
               return _actualValue();
            },
            write: function(newValue) {
               return _tempValue = newValue;
            }
         });
      result.commit = function() {
         if (_tempValue !== _actualValue()) {
            return _actualValue(_tempValue);
         }
      };
      result.reset = function() {
         _actualValue.valueHasMutated();
         return _tempValue = _actualValue();
      };
      return result;
   };


   ko.protectedObservableItem = function (item) {
      for (var param in item) {
         if (item.hasOwnProperty(param)) {
            this[param] = ko.protectedObservable(item[param]);
         }
      }

      this.commit = function () {
         for (var property in this) {
            if (this.hasOwnProperty(property) && this[property].commit) this[property].commit();
         }
      }
   };

   ko.toProtectedObservableItemArray = function (sourceArray) {
      var drillItems = ko.utils.arrayMap(sourceArray, function (item) {
         return new ko.protectedObservableItem(item);
      })
      return drillItems;
   };
});


     
      