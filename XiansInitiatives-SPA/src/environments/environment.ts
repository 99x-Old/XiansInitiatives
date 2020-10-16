// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001/api/',
  clientUrl: 'http://localhost:4200/',
  uploadUrl: 'https://74902.cke-cs.com/easyimage/upload/',
  tokenUrl: 'https://74902.cke-cs.com/token/dev/fb3db722ef2f6051ebcf1ff01960a874871c251ac0ac179bbb4dcc0ae6c6',
  webSocketUrl: 'wss://74902.cke-cs.com/ws'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
