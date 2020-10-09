#IdentityMvcDemo

simple demo use a memory repository, to show how to extension asp.net identity.
a simple demo for login validate code gotchas.

## change list

- 20201009 add validate code gotcha

## codes in Models folder:

- /DemoUsers: customize [Microsoft.AspNet.Identity.IUser] with [DemoUser], and with a memory repository.
- /MyIdentity: customize [Microsoft.AspNet.Identity.UserManager] with [MyUserManager]
- /MySignIn: customize [Microsoft.AspNet.Identity.Owin.SignInManager] with [MySignInManager]

for more infomations, codes: [IdentityMvcDemo](https://github.com/congzw/IdentityMvcDemo)