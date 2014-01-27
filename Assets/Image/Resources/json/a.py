#!/usr/bin/python
# -*- coding: utf-8 -*-
import json
import os
filenames = ["DeaSword","TaeSword","B_M_Sword","Helmet","L_Armor","NCL","BU","hea","bod","hand"]

for filename in filenames:
    if not os.path.exists('%s.json'%filename):
        print 'no'
    with open('%s.json'%filename,'r') as fp:
        ss = json.loads(fp.read())
    price_limit = 1000
    for s in ss:
        s['HP'] = 10
        s['price'] = price_limit
        price_limit += 3000

    with open('%s.json'%filename,'w') as fp:
        fp.write(json.dumps(ss))
