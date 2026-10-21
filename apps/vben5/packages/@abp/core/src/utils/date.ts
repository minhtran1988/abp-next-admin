import { formatDate, isNumber } from '@vben/utils';

/**
 * Independent time operation tool to facilitate subsequent switch to dayjs
 */
import dayjs from 'dayjs';

const DATE_TIME_FORMAT: Format = 'YYYY-MM-DD HH:mm:ss';
const DATE_FORMAT: Format = 'YYYY-MM-DD';

type FormatDate = Date | dayjs.Dayjs | number | string;
type Format =
  | 'HH'
  | 'HH:mm'
  | 'HH:mm:ss'
  | 'YYYY'
  | 'YYYY-MM'
  | 'YYYY-MM-DD'
  | 'YYYY-MM-DD HH'
  | 'YYYY-MM-DD HH:mm'
  | 'YYYY-MM-DD HH:mm:ss'
  | (string & {});

/**
 * @zh_CN formatting time
 * @param date the time to be formatted
 * @param format format string, refer to dayJs documentation
 * @returns returns the formatted time string
 */
export function formatToDateTime(
  date?: FormatDate,
  format: Format = DATE_TIME_FORMAT,
): string {
  return formatDate(date, format);
}

/**
 * @zh_CN format date
 * @param date the date to be formatted
 * @param format format string, refer to dayJs documentation
 * @returns returns the formatted date string
 */
export function formatToDate(
  date?: FormatDate,
  format: Format = DATE_FORMAT,
): string {
  if (isNumber(date)) {
    date *= 1000;
  }
  return formatDate(date, format);
}

/**
 * @zh_CN Convert to date
 * @param date date/string
 * @returns date
 */
export function toDate(date: dayjs.ConfigType): dayjs.Dayjs {
  return dayjs(date);
}

/**
 * @zh_CN Get the specified date
 * @param days Number of days
 * @returns Returns the date after the specified number of days
 */
export function getAppointDate(days: number): dayjs.Dayjs {
  const today = new Date(); // Get the current date
  const tomorrow = new Date(
    today.getFullYear(),
    today.getMonth(),
    today.getDate() + days,
  );
  return dayjs(tomorrow);
}

/**
 * @zh_CN Get the first day of this week
 * @returns Returns the first day of this week
 */
export function firstDayOfWeek(): dayjs.Dayjs {
  const now = new Date();
  const today = now.getDay();
  const dayOffset = today === 0 ? -6 : 1 - today;
  const monday = new Date(now);
  monday.setDate(now.getDate() + dayOffset);
  return dayjs(
    new Date(monday.getFullYear(), monday.getMonth(), monday.getDate()),
  );
}

/**
 * @zh_CN Get the last day of this week
 * @returns returns the last day of the week
 */
export function lastDayOfWeek(): dayjs.Dayjs {
  return firstDayOfWeek().add(6, 'day');
}

/**
 * @zh_CN Get the first day of the month
 * @returns returns the first day of the month
 */
export function firstDayOfMonth(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(new Date(now.getFullYear(), now.getMonth(), 1));
}

/**
 * @zh_CN Get the last day of the month 00:00:00
 * @returns returns the last day of the month 00:00:00
 */
export function lastDayOfMonth(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(new Date(now.getFullYear(), now.getMonth() + 1, 0));
}

/**
 * @zh_CN Get the last day of the month 23:59:59
 * @returns returns the last day of the month at 23:59:59
 */
export function lastDateOfMonth(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(
    new Date(now.getFullYear(), now.getMonth() + 1, 0, 23, 59, 59, 999),
  );
}

/**
 * @zh_CN Get the first day of last month
 * @returns Returns the first day of last month
 */
export function firstDayOfLastMonth(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(new Date(now.getFullYear(), now.getMonth() - 1, 1));
}

/**
 * @zh_CN Get the last day of the previous month at 23:59:59
 * @returns Returns 23:59:59 of the last day of the previous month
 */
export function lastDateOfLastMonth(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(
    new Date(now.getFullYear(), now.getMonth(), 0, 23, 59, 59, 999), // The 0th day of the current month = the last day of the previous month
  );
}

/**
 * @zh_CN Get the first day of this year
 * @returns returns the first day of this year
 */
export function firstDayOfYear(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(new Date(now.getFullYear(), 0, 1));
}

/**
 * @zh_CN Get the last day of this year
 * @returns returns the last day of this year
 */
export function lastDayOfYear(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(new Date(now.getFullYear(), 11, 31));
}

/**
 * @zh_CN Get the first day of the last six months
 * @returns Returns the first day of the last six months
 */
export function firstDayOfLastHalfYear(): dayjs.Dayjs {
  const now = new Date();
  return dayjs(new Date(now.getFullYear(), now.getMonth() - 6, 1));
}

/**
 * @zh_CN Get the first day of this quarter
 * @returns Returns the first day of this quarter
 */
export function firstDayOfQuarter(): dayjs.Dayjs {
  const now = new Date();
  const quarter = Math.floor(now.getMonth() / 3) + 1;
  return dayjs(new Date(now.getFullYear(), (quarter - 1) * 3, 1));
}

/**
 * @zh_CN Get the last day of this quarter
 * @returns returns the last day of this quarter
 */
export function lastDayOfQuarter(): dayjs.Dayjs {
  const now = new Date();
  const quarter = Math.floor(now.getMonth() / 3) + 1;
  return dayjs(new Date(now.getFullYear(), quarter * 3, 0));
}

export const dateUtil = dayjs;
