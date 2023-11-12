const SHARED_CONFIG = {
  secure: false,
  proxyTimeout: 99999999,
  timeout: 18000000,
};

module.exports = function proxyConfig({ backendTarget }) {
  return [
    {
      ...SHARED_CONFIG,
      context: ['/api/'],
      target: backendTarget,
    },
  ];
};
