import axios from '@/libs/api.request'

export const login = ({ userName, password }) => {
  const data = {
    username: userName,
    password: password
  }

  return axios.request({
    url: 'token',
    data,
    // 将object 转换为 formdata传输
    transformRequest: [function (data) {
      let ret = ''
      for (let it in data) {
        ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
      }
      return ret
    }],
    method: 'post'
  })
}

export const getUserInfo = () => {
  return axios.request({
    url: 'v1/user',
    method: 'get'
  })
}

export const logout = () => {
  return axios.request({
    url: 'v1/account',
    method: 'post'
  })
}
