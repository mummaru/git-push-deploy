//@auth
//@req(name, url, branch, targetEnv, login, password)

targetEnv = targetEnv.toString().split(".")[0];
var params = {
   name: name,
   envName: "${env.envName}",
   env: targetEnv,
   nodeId: "${nodes.build.first.id}",
   session: session,
   type: "git",
   context: "ROOT",
   url: url,
   branch: branch,
   keyId: null,
   login: login,
   password: password,
   autoupdate: false,
   interval: 1,
   autoResolveConflict: true
}

//remove the old project
var projectId = parseInt("${nodes.build.first.customitem.projects[0].id}", 10);
if (!isNaN(projectId)) {
   var resp = jelastic.env.build.RemoveProject(params.envName, params.session, params.nodeId, projectId);
   if (resp.result != 0) return resp;
}

//add and build new project 
var resp = jelastic.env.build.AddProject(params.envName, params.session, params.nodeId, params.name, params.type, params.url, params.keyId, params.login, params.password, params.env, params.context, params.branch, params.autoupdate, params.interval, params.autoResolveConflict);
//if (resp.result != 0) return resp;

//resp = jelastic.env.build.BuildProject(params.envName, params.session, params.nodeId, resp.id);
return resp;