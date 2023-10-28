export enum ApiConfig {
  //host
  protocol = 'https',
  host = 'localhost',
  port = '5001',
  api = 'api',
  route = `${protocol}://${host}:${port}/${api}/`,

  //endpoints
  products = 'products',
  purhcases = 'purhcases',
  session = 'session',
  shoppingCart = 'shopping-cart',
  signup = 'signup',
  users = 'users',
}
