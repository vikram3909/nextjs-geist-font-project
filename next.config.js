/** @type {import('next').NextConfig} */
const nextConfig = {
  experimental: {
    turbo: { enabled: false } // Disable Turbopack
  },
  async headers() {
    return [
      {
        source: '/:path*',
        headers: [
          { key: 'Access-Control-Allow-Origin', value: '*' },
          { key: 'Access-Control-Allow-Methods', value: 'GET,POST,PUT,DELETE,OPTIONS' },
          { key: 'Access-Control-Allow-Headers', value: 'Content-Type' },
        ],
      },
    ];
  },
  webpack: (config) => {
    config.resolve.fallback = { fs: false };
    return config;
  },
}

module.exports = nextConfig;
