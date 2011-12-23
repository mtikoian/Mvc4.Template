jQuery(function () {
   var $ = jQuery;
   var SS = null || {};

   SS.DateHelper = function () {
      this.getFirstDayOfMonth = function (date) {
         var d = new Date(date);
         d.setDate(1);
         return d;
      };
      this.getLastDayOfMonth = function (date) {
         var d = new Date(date);
         d.setMonth(date.getMonth() + 1);
         d.setDate(0);
         return d;
      };
   };

   SS.CalendarHelper = function () {
      this.remove_extra_trailing_day_spaces = function (last_day) {
         $(".day.next_month").css('display', 'inline-block');
         for (i = 0; i < last_day; i = i + 1) {
            $(".day.next_month")[i].style.display = 'none';
         }
      };

      this.remove_extra_trailing_dates = function (last_date) {
         $(".day.current_month").css('display', 'inline-block');
         for (i = 31; i > last_date; i = i - 1) {
            $(".day.current_month")[i - 1].style.display = 'none';
         }
      };

      this.remove_extra_leading_day_spaces = function (first_day) {
         $(".day.previous_month").css('display', 'inline-block');
         for (i = 0; i < 6 - first_day; i = i + 1) {
            $(".day.previous_month")[i].style.display = 'none';
         }
      };

      var month = new Array(12);
      month[0] = "January";
      month[1] = "February";
      month[2] = "March";
      month[3] = "April";
      month[4] = "May";
      month[5] = "June";
      month[6] = "July";
      month[7] = "August";
      month[8] = "September";
      month[9] = "October";
      month[10] = "November";
      month[11] = "December";

      this.setCalendar = function (date) {
         $('#calendar #date-input').val(date.toLocaleString());
         $('#calendar').css('visibility', 'hidden');
         $("#calendar .day").removeClass('selected');
         var dateHelper = new SS.DateHelper();

         var first_day = dateHelper.getFirstDayOfMonth(date).getDay();
         var last_date = dateHelper.getLastDayOfMonth(date);
         var last_day = last_date.getDay();

         $('#calendar .month').html(month[last_date.getMonth()] + ' - ' + last_date.getFullYear());

         this.remove_extra_leading_day_spaces(first_day);
         this.remove_extra_trailing_dates(last_date.getDate());
         this.remove_extra_trailing_day_spaces(last_day);

         $('#calendar').css('visibility', 'visible');
      };
   };
   
   var calendar_helper = new SS.CalendarHelper();
   var first_of_this_month = new Date(new Date().setDate(1));
   calendar_helper.setCalendar(first_of_this_month);

   $('#calendar .navigation .move.next').click(function () {
      var currentSetting = new Date($('#calendar #date-input').val());
      currentSetting.setMonth(currentSetting.getMonth() + 1);
      calendar_helper.setCalendar(currentSetting);
   });

   $('#calendar .navigation .move.previous').click(function () {
      var currentSetting = new Date($('#calendar #date-input').val());
      currentSetting.setMonth(currentSetting.getMonth() - 1);
      calendar_helper.setCalendar(currentSetting);
   });

   $(".day.current_month").click(function () {
      $(".day").removeClass('selected');
      $(this).addClass('selected');
   });
});