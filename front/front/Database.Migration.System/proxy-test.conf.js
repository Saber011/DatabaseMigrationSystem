const proxyConfig = require('./proxy-config');

module.exports = proxyConfig({
  backendTarget: 'http://localhost:5000',
});
