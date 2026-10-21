import { useSettings } from '/@/hooks/abp/useSettings';
/**
 * Taken from https://www.html5tricks.com/demo/js-passwd-generator/index.html
 * Please contact us for deletion if there is any infringement.
 */
export function useRandomPassword() {
  const randomFunc = {
    lower: getRandomLower,
    upper: getRandomUpper,
    number: getRandomNumber,
    symbol: getRandomSymbol,
    defaultNumber: getRandomNumber,
  };

  function getRandomLower() {
    return String.fromCharCode(Math.floor(Math.random() * 26) + 97);
  }

  function getRandomUpper() {
    return String.fromCharCode(Math.floor(Math.random() * 26) + 65);
  }

  function getRandomNumber() {
    return String.fromCharCode(Math.floor(Math.random() * 10) + 48);
  }

  function getRandomSymbol() {
    const symbols = '~!@#$%^&*()_+{}":?><;.,';
    return symbols[Math.floor(Math.random() * symbols.length)];
  }

  function generatePassword() {
    const { settingProvider } = useSettings();
    // Generate a random password based on configuration items
    // Password length
    const length = settingProvider.getNumber('Abp.Identity.Password.RequiredLength');
    // requires lowercase letters
    const lower = settingProvider.isTrue('Abp.Identity.Password.RequireLowercase');
    // Requires uppercase letters
    const upper = settingProvider.isTrue('Abp.Identity.Password.RequireUppercase');
    // requires numbers
    const number = settingProvider.isTrue('Abp.Identity.Password.RequireDigit');
    // requires symbols
    const symbol = settingProvider.isTrue('Abp.Identity.Password.RequireNonAlphanumeric');
    // generate numbers by default
    const defaultNumber = !lower && !upper && !number && !symbol;

    let generatedPassword = '';
    const typesArr = [{ lower }, { upper }, { number }, { symbol }, { defaultNumber }].filter(
      (item) => Object.values(item)[0],
    );
    for (let i = 0; i < length; i++) {
      typesArr.forEach((type) => {
        const funcName = Object.keys(type)[0];
        generatedPassword += randomFunc[funcName]();
      });
    }
    return generatedPassword.slice(0, length);
  }

  return { generatePassword };
}
