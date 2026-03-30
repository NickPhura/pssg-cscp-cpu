export const POSTAL_CODE: RegExp =
  /^[A-Za-z][0-9][A-Za-z][ ]?[0-9][A-Za-z][0-9] *$/;
export const WORD: RegExp = /\w{2,}/; // there must be at least 2 valid characters
export const EMAIL: RegExp =
  /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})) *$/;
export const PHONE_NUMBER: RegExp =
  /^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/;
export const LETTERS_SPACES: RegExp = /^[a-zA-Z|\s]{2,}$/;
export const NAME_REGEX: RegExp = /^[a-zA-Z|\s'.-]{2,}$/;
export const TIME: RegExp = /^(0?[1-9]|1[0-2])[0-5][0-9]$/;

// String versions for HTML [pattern] bindings — no regex delimiters, character classes
// fixed for browser /v (Unicode Sets) mode where ( ) | must be escaped inside [...],
// and - must be \- when between two characters (not at start/end of class).
export const POSTAL_CODE_PATTERN: string = POSTAL_CODE.source;
// [.-] → [.\-] to avoid invalid range in /v mode
export const PHONE_NUMBER_PATTERN: string = String.raw`^(?:(?:\+?1\s*(?:[.\-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.\-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.\-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$`;
export const TIME_PATTERN: string = TIME.source;
// ( ) inside the character class must be \( \) in /v mode
export const EMAIL_PATTERN: string = String.raw`^(([^<>\(\)\[\]\\.,;:\s@"]+(\.[^<>\(\)\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})) *$`;
// | inside character classes must be \| in /v mode
export const NAME_REGEX_PATTERN: string = String.raw`^[a-zA-Z\|\s'.\-]{2,}$`;
export const LETTERS_SPACES_PATTERN: string = String.raw`^[a-zA-Z\|\s]{2,}$`;
