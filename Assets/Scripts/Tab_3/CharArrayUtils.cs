using System;

namespace UIDeveloperTest.Tab_3
{
    public static class CharArrayUtils
    {
        private const char _timeDivider = ':';
        private const char _dateAndTimeDivider = ' ';
        private const char _dateDivider = '.';

        private const char _zeroCharacter = '0';
        private const char _minusCharacter = '-';

        private const int _addZeroThreashold = 10;

        public const int RequiredArrayLength = 19;

        /// <summary>
        /// Set array of characters from DateTime without memory allocation
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="datetime"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void SetCharArrayFromDateTime(char[] arr, DateTime datetime)
        {
            if (arr.Length < RequiredArrayLength)
                throw new ArgumentOutOfRangeException($"Array length must be equal or greater {RequiredArrayLength}");

            int charIndex = 0;

            // set characters to array from date time in reverse direction

            // set seconds
            var seconds = datetime.Second;
            SetIntegerToArray(arr, seconds, ref charIndex);

            // if seconds is less than 10 add 0 for better look
            if (seconds < _addZeroThreashold)
                arr[charIndex++] = _zeroCharacter;

            // add : char
            arr[charIndex++] = _timeDivider;

            // set minutes
            var minutes = datetime.Minute;
            SetIntegerToArray(arr, minutes, ref charIndex);

            // same as seconds
            if (minutes < _addZeroThreashold)
                arr[charIndex++] = _zeroCharacter;

            arr[charIndex++] = _timeDivider;

            // set hours
            var hours = datetime.Hour;
            SetIntegerToArray(arr, hours, ref charIndex);
            if (hours < _addZeroThreashold)
                arr[charIndex++] = _zeroCharacter;

            // add space between date and time
            arr[charIndex++] = _dateAndTimeDivider;

            // set date
            SetIntegerToArray(arr, datetime.Year, ref charIndex);
            arr[charIndex++] = _dateDivider;
            SetIntegerToArray(arr, datetime.Month, ref charIndex);
            arr[charIndex++] = _dateDivider;
            SetIntegerToArray(arr, datetime.Day, ref charIndex);

            // fill left indexes with empty characters 
            for (int i = charIndex; i < RequiredArrayLength; i++)
            {
                arr[i] = char.MinValue;
            }

            // reverse array
            Array.Reverse(arr, 0, charIndex);
        }

        private static void SetIntegerToArray(char[] arr, int number, ref int charIndex)
        {
            const int ten = 10;

            if (number == 0)
                arr[charIndex++] = _zeroCharacter;
            else
            {
                bool isNegativeNumber = number < 0;
                if (isNegativeNumber)
                    number = -number;

                // adding number characters in reverse direction
                while (number > 0)
                {
                    arr[charIndex++] = (char)(_zeroCharacter + number % ten);
                    number /= ten;
                }

                if (isNegativeNumber)
                    arr[charIndex++] = _minusCharacter;
            }
        }
    }
}