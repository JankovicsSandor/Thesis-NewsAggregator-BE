db.createUser({
  user: 'client',
  pwd: 'clientPW',
  roles: [
    {
      role: 'readWrite',
      db: 'NewsGroup'
    }
  ]
})